using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Map001RuntimeDirector : MonoBehaviour
{
    public Map001PlayerController player;
    public Map001EnemyController[] enemies;
    public Animator heroAnimator;
    public GameObject hero;
    public Map001Hud hud;
    public Transform exitPoint;
    public GameObject iceEffect;

    private bool pursuitStarted;
    private float pursuitTime;
    private bool heroArrived;
    private float lastClickTime = -1f;
    private bool complete;

    private void Start()
    {
        foreach (var enemy in enemies) if (enemy != null) enemy.gameObject.SetActive(false);
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
            foreach (var enemy in enemies) if (enemy != null) enemy.gameObject.SetActive(true);
            pursuitTime = float.PositiveInfinity;
        }
        if (!heroArrived && player.transform.position.x >= 47.5f)
        {
            heroArrived = true;
            if (hero != null) hero.SetActive(true);
            heroAnimator?.SetTrigger("Cast");
            foreach (var enemy in enemies) if (enemy != null) enemy.Freeze();
            if (iceEffect != null) StartCoroutine(ShowIce());
            hud?.ShowMessage("Nhọc con hãy mau chạy đi", 4.2f);
        }

        float exitDistance = exitPoint != null ? Vector2.Distance(player.transform.position, exitPoint.position) : 999f;
        hud?.SetExitHint(exitDistance < 6f);
        var mouse = Mouse.current;
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            if (Time.unscaledTime - lastClickTime < 0.34f && exitDistance < 7f) Complete();
            lastClickTime = Time.unscaledTime;
        }
        if (player.transform.position.x > 95.25f) Complete();
    }

    private System.Collections.IEnumerator ShowIce()
    {
        iceEffect.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        iceEffect.SetActive(false);
    }

    private void Complete()
    {
        if (complete) return;
        complete = true;
        hud?.ShowCompletion("Khu Đất Sau Nhà");
    }
}
