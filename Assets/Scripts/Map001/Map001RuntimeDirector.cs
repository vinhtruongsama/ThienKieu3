using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public sealed class Map001RuntimeDirector : MonoBehaviour
{
    private static readonly float[] FormationOffsets = { -3.0f, -7.0f, -11.0f };

    public Map001PlayerController player;
    public Map001EnemyController[] enemies;
    public Animator heroAnimator;
    public GameObject hero;
    public Map001Hud hud;
    public Transform exitPoint;
    public GameObject iceEffect;

    private bool pursuitStarted;
    private float pursuitTime;
    private bool gorillaScreenshotCaptured;
    private bool enemiesReleased;
    private int formationOrder;
    private float nextFormationShuffleAt;
    private float nextPounceAt;
    private Map001EnemyController activePouncer;
    private bool heroArrived;
    private float lastClickTime = -1f;
    private bool complete;

    private void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Map001EnemyController enemy = enemies[i];
            if (enemy == null) continue;
            enemy.Configure(player, this, enemies, i);
            enemy.gameObject.SetActive(false);
        }
        if (hero != null) hero.SetActive(false);
        if (iceEffect != null) iceEffect.SetActive(false);
    }

    private void Update()
    {
        if (player == null) return;
        if (player.HasMoved && !pursuitStarted)
        {
            pursuitStarted = true;
            pursuitTime = Time.time + 1f;
        }
        if (pursuitStarted && Time.time >= pursuitTime)
        {
            ReleaseGorillas();
            pursuitTime = float.PositiveInfinity;
        }
        if (enemiesReleased && !heroArrived) UpdateGorillaFormation();
#if UNITY_EDITOR
        if (pursuitStarted && float.IsPositiveInfinity(pursuitTime) && !gorillaScreenshotCaptured && player.IsEditorTestAutopilot && player.transform.position.x >= 46f)
        {
            gorillaScreenshotCaptured = true;
            MapTestScreenshot.Capture(this, "20261102-MAP001-KhiDotXuatHienCungLuc.png");
        }
#endif
        if (!heroArrived && player.transform.position.x >= 47.5f)
        {
            heroArrived = true;
            if (hero != null) hero.SetActive(true);
            // Unity's null-conditional operator does not handle an unassigned serialized
            // UnityEngine.Object reference. Use Unity's overloaded null check instead.
            if (heroAnimator != null) heroAnimator.SetTrigger("Cast");
            foreach (var enemy in enemies) if (enemy != null) enemy.Freeze();
            if (iceEffect != null) StartCoroutine(ShowIce());
            hud?.ShowMessage("Nhọc con hãy mau chạy đi", 4.2f);
            Debug.Log("MAP-001: Anh Hùng xuất hiện, đóng băng ba Khỉ Đột.");
#if UNITY_EDITOR
            if (player.IsEditorTestAutopilot)
                MapTestScreenshot.Capture(this, "20261102-MAP001-AnhHungDungChan.png");
#endif
        }

        float exitDistance = exitPoint != null ? Vector2.Distance(player.transform.position, exitPoint.position) : 999f;
        hud?.SetExitHint(exitDistance < 6f);
        var mouse = Mouse.current;
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            if (Time.unscaledTime - lastClickTime < 0.34f && exitDistance < 7f) Complete();
            lastClickTime = Time.unscaledTime;
        }
        if (exitPoint != null && player.transform.position.x >= exitPoint.position.x - 1.0f)
            Complete();
    }

    private System.Collections.IEnumerator ShowIce()
    {
        iceEffect.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        iceEffect.SetActive(false);
    }

    private void ReleaseGorillas()
    {
        if (enemiesReleased) return;
        enemiesReleased = true;

        foreach (Map001EnemyController enemy in enemies)
            if (enemy != null) enemy.gameObject.SetActive(true);

        IgnoreGorillaCollisions();
        foreach (Map001EnemyController enemy in enemies)
            if (enemy != null) enemy.BeginAppearance();

        nextFormationShuffleAt = Time.time + 7f;
        Debug.Log("MAP-001: Ba Khỉ Đột cùng nhảy ra và gầm gừ trước khi rượt đuổi.");
    }

    private void IgnoreGorillaCollisions()
    {
        Collider2D playerCollider = player != null ? player.GetComponent<Collider2D>() : null;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null) continue;
            Collider2D enemyCollider = enemies[i].GetComponent<Collider2D>();
            if (enemyCollider == null) continue;
            if (playerCollider != null) Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);

            for (int j = 0; j < i; j++)
            {
                if (enemies[j] == null) continue;
                Collider2D otherCollider = enemies[j].GetComponent<Collider2D>();
                if (otherCollider != null) Physics2D.IgnoreCollision(enemyCollider, otherCollider, true);
            }
        }
    }

    private void UpdateGorillaFormation()
    {
        if (player == null || enemies == null || enemies.Length == 0) return;

        float facing = Mathf.Sign(player.transform.localScale.x);
        if (Mathf.Abs(facing) < 0.01f) facing = 1f;
        bool everyoneChasing = true;
        bool anyReordering = false;

        for (int i = 0; i < enemies.Length; i++)
        {
            Map001EnemyController enemy = enemies[i];
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;
            if (!enemy.IsInChasePhase) everyoneChasing = false;
            if (enemy.IsReordering) anyReordering = true;
            float offset = FormationOffsets[(i + formationOrder) % FormationOffsets.Length];
            enemy.SetFormationTarget(player.transform.position.x + facing * offset);
        }

        if (!everyoneChasing || anyReordering || activePouncer != null || Time.time < nextFormationShuffleAt) return;

        formationOrder = (formationOrder + 1) % FormationOffsets.Length;
        for (int i = 0; i < enemies.Length; i++)
        {
            Map001EnemyController enemy = enemies[i];
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;
            float offset = FormationOffsets[(i + formationOrder) % FormationOffsets.Length];
            float targetX = player.transform.position.x + facing * offset;
            enemy.ScheduleShuffleHop(targetX, i * 0.18f);
        }
        nextFormationShuffleAt = Time.time + UnityEngine.Random.Range(6.2f, 8.1f);
        Debug.Log("MAP-001: Đàn Khỉ Đột đổi thứ tự đội hình bằng những cú nhảy vượt nhau.");
    }

    public bool TryClaimPounce(Map001EnemyController enemy)
    {
        if (enemy == null || activePouncer != null || Time.time < nextPounceAt) return false;
        activePouncer = enemy;
        return true;
    }

    public void FinishPounce(Map001EnemyController enemy)
    {
        if (activePouncer != enemy) return;
        activePouncer = null;
        nextPounceAt = Time.time + UnityEngine.Random.Range(1.6f, 2.4f);
    }

    private void Complete()
    {
        if (complete) return;
        complete = true;
        hud?.ShowCompletion("Khu Đất Sau Nhà");
        Debug.Log("MAP-001: Người chơi đến lối ra; chuyển sang Khu Đất Sau Nhà.");
        StartCoroutine(LoadNextMap());
    }

    private IEnumerator LoadNextMap()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("KhuDatSauNha");
    }
}
