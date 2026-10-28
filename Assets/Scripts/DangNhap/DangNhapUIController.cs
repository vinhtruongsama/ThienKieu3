using UnityEngine;
using UnityEngine.UI;

public sealed class DangNhapUIController : MonoBehaviour
{
    [SerializeField] private InputField truongMatHieu;
    [SerializeField] private GameObject iconMatMo;
    [SerializeField] private GameObject iconMatDong;

    public void ToggleMatHieu()
    {
        if (truongMatHieu == null) return;

        bool dangAn = truongMatHieu.contentType == InputField.ContentType.Password;
        truongMatHieu.contentType = dangAn ? InputField.ContentType.Standard : InputField.ContentType.Password;
        truongMatHieu.ForceLabelUpdate();

        if (iconMatMo != null) iconMatMo.SetActive(dangAn);
        if (iconMatDong != null) iconMatDong.SetActive(!dangAn);
    }
}
