using System.Collections;
using System.IO;
using UnityEngine;

public static class MapTestScreenshot
{
    public static void Capture(MonoBehaviour owner, string fileName)
    {
#if UNITY_EDITOR
        if (owner != null) owner.StartCoroutine(CaptureAfterFrame(fileName));
#endif
    }

#if UNITY_EDITOR
    private static IEnumerator CaptureAfterFrame(string fileName)
    {
        yield return new WaitForEndOfFrame();
        string projectRoot = Directory.GetParent(Application.dataPath).FullName;
        string folder = Path.Combine(projectRoot, "AI", ".ScreenShorts");
        Directory.CreateDirectory(folder);
        string path = Path.Combine(folder, fileName);
        ScreenCapture.CaptureScreenshot(path);

        for (int i = 0; i < 90 && !File.Exists(path); i++) yield return null;
        if (File.Exists(path)) Debug.Log("Ảnh chơi thử đã lưu: " + path);
        else Debug.LogWarning("Chưa tạo được ảnh chơi thử: " + path);
    }
#endif
}
