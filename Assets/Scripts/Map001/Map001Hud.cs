using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class Map001Hud : MonoBehaviour
{
    public Map001PlayerController player;
    public Text healthText;
    public Image healthFill;
    public Text objectiveText;
    public Text messageText;
    public GameObject messagePanel;
    public GameObject exitHint;
    public GameObject completionPanel;
    public Text completionText;
    private Coroutine messageRoutine;

    private void Update()
    {
        if (player == null) return;
        if (healthText != null) healthText.text = $"Sinh lực  {Mathf.CeilToInt(player.Health)} / 100";
        if (healthFill != null) healthFill.fillAmount = player.Health / 100f;
        if (objectiveText != null) objectiveText.text = player.transform.position.x < 88f ? "Bám theo dấu vàng đến lối ra" : "Đến gần lối ra";
    }

    public void SetExitHint(bool visible)
    {
        if (exitHint != null) exitHint.SetActive(visible && (completionPanel == null || !completionPanel.activeSelf));
    }

    public void ShowMessage(string message, float seconds)
    {
        if (messageText != null) messageText.text = message;
        if (messageRoutine != null) StopCoroutine(messageRoutine);
        messageRoutine = StartCoroutine(MessageFor(seconds));
    }

    private IEnumerator MessageFor(float seconds)
    {
        if (messagePanel != null) messagePanel.SetActive(true);
        yield return new WaitForSeconds(seconds);
        if (messagePanel != null) messagePanel.SetActive(false);
    }

    public void ShowCompletion(string nextArea)
    {
        if (completionText != null) completionText.text = "Đường đã thông\n" + nextArea;
        if (completionPanel != null) completionPanel.SetActive(true);
        if (exitHint != null) exitHint.SetActive(false);
    }
}
