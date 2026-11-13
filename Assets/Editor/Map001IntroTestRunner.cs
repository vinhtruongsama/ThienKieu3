using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class Map001IntroTestRunner
{
    private const string ScenePath = "Assets/Scenes/ThungLungHoang.unity";
    private const string SessionKey = "ThienKieu.Map001IntroTest.Autopilot";

    static Map001IntroTestRunner()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeChanged;
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    [MenuItem("ThienKieu/Chạy thử mạch mở đầu (tự chạy)")]
    public static void RunIntroSequence()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.LogWarning("Hãy dừng Play Mode trước khi bắt đầu lượt thử mạch mở đầu.");
            return;
        }

        if (EditorSceneManager.GetActiveScene().path != ScenePath)
            EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);

        SessionState.SetBool(SessionKey, true);
        EditorApplication.isPlaying = true;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
            SessionState.SetBool(SessionKey, false);

        if (state != PlayModeStateChange.EnteredPlayMode || !SessionState.GetBool(SessionKey, false))
            return;

        SessionState.SetBool(SessionKey, false);
        Map001PlayerController player = Object.FindFirstObjectByType<Map001PlayerController>();
        if (player == null)
        {
            Debug.LogError("Không tìm thấy người chơi trong scene MAP-001 để chạy thử.");
            return;
        }

        player.EnableEditorTestAutopilotForIntro();
        Debug.Log("Bắt đầu thử tự động: chạy/nhảy → ba Khỉ Đột → Anh Hùng → Khu Đất Sau Nhà/Sờ Lam.");
    }
}
