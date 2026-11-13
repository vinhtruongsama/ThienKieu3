using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class Map002RuntimeDirector : MonoBehaviour
{
    public GameObject soLam;
    public Text dialogueText;
    public GameObject dialoguePanel;
    public Map001PlayerController player;

    private IEnumerator Start()
    {
        if (soLam != null) soLam.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        yield return new WaitForSeconds(0.45f);

        if (soLam != null) soLam.SetActive(true);
        if (dialogueText != null) dialogueText.text = "Sờ Lam: Phù, may mắn,...";
        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        Debug.Log("MAP-002: Sờ Lam xuất hiện sau khi rời Thung Lũng Hoang.");

#if UNITY_EDITOR
        MapTestScreenshot.Capture(this, "20261030-MAP002-05-SoLamXuatHien.png");
#endif

        yield return new WaitForSeconds(4.5f);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }
}
