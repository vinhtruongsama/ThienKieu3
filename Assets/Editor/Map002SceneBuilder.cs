using System.Linq;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Map002SceneBuilder
{
    private const string ScenePath = "Assets/Scenes/KhuDatSauNha.unity";
    private const string Map001Path = "Assets/Scenes/ThungLungHoang.unity";
    private const string LoginPath = "Assets/Scenes/DangNhap.unity";
    private const string WhiteSpritePath = "Assets/Art/Map/ThungLungHoang/MauTrang.png";
    private const string SoLamCirclePath = "Assets/Art/NPC/SoLamPlaceholderCircle.png";
    private const string PlayerSpritePath = "Assets/Art/NhanVat/NguoiChoiNamSprite.asset";

    [MenuItem("ThienKieu/Dựng scene thử MAP-002 — Khu Đất Sau Nhà")]
    public static void Build()
    {
        Sprite white = AssetDatabase.LoadAssetAtPath<Sprite>(WhiteSpritePath);
        Sprite playerSprite = AssetDatabase.LoadAssetAtPath<Sprite>(PlayerSpritePath);
        RuntimeAnimatorController playerController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Art/NhanVat/Animations/Player.controller");
        Sprite circle = EnsureSoLamPlaceholderCircle();
        if (white == null || playerSprite == null)
        {
            Debug.LogError("Thiếu sprite trắng hoặc sprite Người chơi; không dựng được MAP-002.");
            return;
        }
        if (circle == null) circle = white;

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        RenderSettings.ambientLight = new Color(0.66f, 0.73f, 0.65f);
        Camera camera = BuildCamera(scene);
        BuildGround(white);
        BuildScenery(white, circle);
        Map001PlayerController player = BuildPlayer(playerSprite, playerController, camera);
        GameObject soLam = BuildSoLam(circle, camera);
        BuildHud(player, soLam);

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        AddOpeningScenesToBuildSettings();
        AssetDatabase.SaveAssets();
        Debug.Log("Scene thử MAP-002 đã tạo: " + ScenePath + " (Sờ Lam là hình tạm, chờ duyệt mỹ thuật).");
    }

    private static Sprite EnsureSoLamPlaceholderCircle()
    {
        if (!File.Exists(SoLamCirclePath))
        {
            string editorDirectory = Path.GetDirectoryName(EditorApplication.applicationPath);
            string source = Path.Combine(editorDirectory, "Data", "Resources", "PackageManager", "BuiltInPackages", "com.unity.2d.sprite", "Editor", "ObjectMenuCreation", "DefaultAssets", "Textures", "v2", "Circle.png");
            if (!File.Exists(source))
            {
                Debug.LogError("Không tìm thấy hình tròn mặc định của Unity cho hình tạm Sờ Lam.");
                return null;
            }

            File.Copy(source, SoLamCirclePath);
        }

        AssetDatabase.ImportAsset(SoLamCirclePath, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
        TextureImporter importer = AssetImporter.GetAtPath(SoLamCirclePath) as TextureImporter;
        if (importer != null && (importer.textureType != TextureImporterType.Sprite || importer.spriteImportMode != SpriteImportMode.Single || !importer.alphaIsTransparency || Mathf.Abs(importer.spritePixelsPerUnit - 256f) > 0.01f))
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.spritePixelsPerUnit = 256f;
            importer.alphaIsTransparency = true;
            importer.mipmapEnabled = false;
            importer.filterMode = FilterMode.Bilinear;
            importer.SaveAndReimport();
        }

        return AssetDatabase.LoadAssetAtPath<Sprite>(SoLamCirclePath);
    }

    private static Camera BuildCamera(Scene scene)
    {
        GameObject cameraObject = new GameObject("Camera Khu Đất Sau Nhà", typeof(Camera), typeof(AudioListener), typeof(Map001CameraRig));
        cameraObject.tag = "MainCamera";
        cameraObject.transform.position = new Vector3(7.5f, 4.5f, -10f);
        Camera camera = cameraObject.GetComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = 4.5f;
        camera.backgroundColor = new Color(0.48f, 0.68f, 0.62f, 1f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        Map001CameraRig rig = cameraObject.GetComponent<Map001CameraRig>();
        rig.mapMinX = 0f;
        rig.mapMaxX = 32f;
        rig.mapMinY = 0f;
        rig.mapMaxY = 18f;
        rig.cameraSize = 4.5f;
        rig.verticalOffset = 2.5f;
        return camera;
    }

    private static void BuildGround(Sprite white)
    {
        GameObject ground = CreateSprite("Nền cỏ phẳng", white, new Vector3(16f, -1.65f, 0f), new Vector3(32f, 3.3f, 1f), new Color(0.38f, 0.52f, 0.30f), 0);
        ground.AddComponent<BoxCollider2D>().size = new Vector2(1f, 1f);

        CreateSprite("Bờ cỏ sáng", white, new Vector3(16f, 0.035f, -0.1f), new Vector3(32f, 0.07f, 1f), new Color(0.63f, 0.70f, 0.36f), 1);
        CreateSprite("Nền xa", white, new Vector3(16f, 5.2f, 2f), new Vector3(32f, 8f, 1f), new Color(0.62f, 0.76f, 0.68f), -20);
    }

    private static void BuildScenery(Sprite white, Sprite circle)
    {
        CreateSprite("Hồ nhỏ", circle, new Vector3(20f, 0.25f, 0.6f), new Vector3(8.5f, 1.35f, 1f), new Color(0.24f, 0.63f, 0.72f, 0.92f), -1);
        float[] treeXs = { 9f, 15.5f, 26f };
        for (int i = 0; i < treeXs.Length; i++)
        {
            float x = treeXs[i];
            CreateSprite("Thân cây " + (i + 1), white, new Vector3(x, 2.25f, 0.5f), new Vector3(0.55f, 4.5f, 1f), new Color(0.30f, 0.20f, 0.12f), -3);
            CreateSprite("Tán cây " + (i + 1), circle, new Vector3(x, 4.8f, 0.4f), new Vector3(4.0f, 3.1f, 1f), new Color(0.20f, 0.40f, 0.24f), -2);
        }

        for (int i = 0; i < 7; i++)
        {
            float x = 2.4f + i * 4.1f;
            CreateSprite("Cỏ dại " + (i + 1), white, new Vector3(x, 0.28f, -0.2f), new Vector3(0.10f, 0.52f, 1f), new Color(0.75f, 0.78f, 0.43f), 2);
        }
    }

    private static Map001PlayerController BuildPlayer(Sprite sprite, RuntimeAnimatorController controller, Camera camera)
    {
        GameObject player = new GameObject("Người chơi — đến Khu Đất Sau Nhà");
        player.transform.position = new Vector3(2.8f, 0.02f, 0f);
        SpriteRenderer renderer = player.AddComponent<SpriteRenderer>(); renderer.sprite = sprite; renderer.sortingOrder = 10;
        Rigidbody2D body = player.AddComponent<Rigidbody2D>(); body.gravityScale = 4.2f; body.mass = 1.1f; body.freezeRotation = true;
        BoxCollider2D box = player.AddComponent<BoxCollider2D>(); box.size = new Vector2(0.78f, 1.95f); box.offset = new Vector2(0f, 0.98f);
        Animator animator = player.AddComponent<Animator>(); animator.runtimeAnimatorController = controller;
        Map001PlayerController movement = player.AddComponent<Map001PlayerController>();
        camera.GetComponent<Map001CameraRig>().target = player.transform;
        return movement;
    }

    private static GameObject BuildSoLam(Sprite circle, Camera camera)
    {
        GameObject root = new GameObject("Sờ Lam — hình tạm chờ duyệt");
        root.transform.position = new Vector3(7.5f, 0.72f, 0f);
        root.AddComponent<Map002SoLamFloat>();
        CreateChildSprite(root.transform, "Thân tròn", circle, Vector3.zero, new Vector3(1.55f, 1.34f, 1f), new Color(0.30f, 0.79f, 0.76f, 0.96f), 11);
        CreateChildSprite(root.transform, "Mắt trái", circle, new Vector3(-0.28f, 0.13f, -0.03f), new Vector3(0.22f, 0.25f, 1f), Color.white, 12);
        CreateChildSprite(root.transform, "Mắt phải", circle, new Vector3(0.28f, 0.13f, -0.03f), new Vector3(0.22f, 0.25f, 1f), Color.white, 12);
        CreateChildSprite(root.transform, "Tròng trái", circle, new Vector3(-0.27f, 0.10f, -0.06f), new Vector3(0.095f, 0.12f, 1f), new Color(0.06f, 0.11f, 0.12f), 13);
        CreateChildSprite(root.transform, "Tròng phải", circle, new Vector3(0.29f, 0.10f, -0.06f), new Vector3(0.095f, 0.12f, 1f), new Color(0.06f, 0.11f, 0.12f), 13);
        CreateChildSprite(root.transform, "Miệng cười", circle, new Vector3(0f, -0.20f, -0.07f), new Vector3(0.23f, 0.055f, 1f), new Color(0.09f, 0.30f, 0.29f), 13);

        GameObject label = new GameObject("Nhãn tên Sờ Lam", typeof(RectTransform), typeof(Canvas));
        label.transform.SetParent(root.transform, false);
        label.transform.localPosition = new Vector3(0f, 1.15f, -0.1f);
        label.transform.localScale = Vector3.one * 0.01f;
        Canvas labelCanvas = label.GetComponent<Canvas>(); labelCanvas.renderMode = RenderMode.WorldSpace; labelCanvas.worldCamera = camera; labelCanvas.sortingOrder = 20;
        RectTransform rect = label.GetComponent<RectTransform>(); rect.sizeDelta = new Vector2(280f, 52f);
        GameObject labelText = new GameObject("Tên", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text)); labelText.transform.SetParent(label.transform, false);
        RectTransform textRect = labelText.GetComponent<RectTransform>(); textRect.anchorMin = Vector2.zero; textRect.anchorMax = Vector2.one; textRect.offsetMin = Vector2.zero; textRect.offsetMax = Vector2.zero;
        Text text = labelText.GetComponent<Text>(); text.text = "SỜ LAM"; text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = 26; text.alignment = TextAnchor.MiddleCenter; text.color = Color.white;
        text.fontStyle = FontStyle.Bold; text.raycastTarget = false;
        return root;
    }

    private static void BuildHud(Map001PlayerController player, GameObject soLam)
    {
        GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
        GameObject canvasObject = new GameObject("UI Khu Đất Sau Nhà", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = canvasObject.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 100;
        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1920f, 1080f);

        GameObject title = CreatePanel("Tên bản đồ", canvas.transform, new Color(0.06f, 0.12f, 0.10f, 0.83f));
        Anchor(title.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(34f, -34f), new Vector2(470f, 72f));
        CreateText(title.transform, "KHU ĐẤT SAU NHÀ", 28, new Color(0.95f, 0.85f, 0.57f), TextAnchor.MiddleCenter);

        GameObject objective = CreatePanel("Mục tiêu", canvas.transform, new Color(0.06f, 0.12f, 0.10f, 0.77f));
        Anchor(objective.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(34f, -120f), new Vector2(470f, 58f));
        CreateText(objective.transform, "Gặp Sờ Lam, rồi đến Nhà Chính", 20, Color.white, TextAnchor.MiddleCenter);

        GameObject dialoguePanel = CreatePanel("Lời Sờ Lam", canvas.transform, new Color(0.04f, 0.12f, 0.15f, 0.93f));
        Anchor(dialoguePanel.GetComponent<RectTransform>(), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -44f), new Vector2(720f, 82f));
        Text dialogue = CreateText(dialoguePanel.transform, "", 27, Color.white, TextAnchor.MiddleCenter);
        dialoguePanel.SetActive(false);

        GameObject note = CreatePanel("Ghi chú tài nguyên", canvas.transform, new Color(0.06f, 0.12f, 0.10f, 0.72f));
        Anchor(note.GetComponent<RectTransform>(), new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-30f, 30f), new Vector2(355f, 54f));
        CreateText(note.transform, "Sờ Lam đang dùng hình tạm", 16, new Color(0.94f, 0.93f, 0.84f), TextAnchor.MiddleCenter);

        Map002RuntimeDirector director = new GameObject("Diễn biến MAP-002").AddComponent<Map002RuntimeDirector>();
        director.player = player; director.soLam = soLam; director.dialogueText = dialogue; director.dialoguePanel = dialoguePanel;
    }

    private static GameObject CreateSprite(string name, Sprite sprite, Vector3 position, Vector3 scale, Color color, int sortingOrder)
    {
        GameObject go = new GameObject(name);
        go.transform.position = position; go.transform.localScale = scale;
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>(); renderer.sprite = sprite; renderer.color = color; renderer.sortingOrder = sortingOrder;
        return go;
    }

    private static GameObject CreateChildSprite(Transform parent, string name, Sprite sprite, Vector3 localPosition, Vector3 localScale, Color color, int sortingOrder)
    {
        GameObject child = CreateSprite(name, sprite, parent.position + localPosition, localScale, color, sortingOrder);
        child.transform.SetParent(parent, true); child.transform.localPosition = localPosition; child.transform.localScale = localScale;
        return child;
    }

    private static GameObject CreatePanel(string name, Transform parent, Color color)
    {
        GameObject go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        go.transform.SetParent(parent, false); go.GetComponent<Image>().color = color;
        return go;
    }

    private static Text CreateText(Transform parent, string value, int size, Color color, TextAnchor align)
    {
        GameObject go = new GameObject("Nhãn", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text)); go.transform.SetParent(parent, false);
        RectTransform rect = go.GetComponent<RectTransform>(); rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = new Vector2(12f, 4f); rect.offsetMax = new Vector2(-12f, -4f);
        Text text = go.GetComponent<Text>(); text.text = value; text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); text.fontSize = size; text.alignment = align; text.color = color; text.raycastTarget = false;
        return text;
    }

    private static void Anchor(RectTransform rect, Vector2 min, Vector2 max, Vector2 position, Vector2 size)
    {
        rect.anchorMin = min; rect.anchorMax = max; rect.pivot = min;
        if (min == Vector2.one) rect.pivot = Vector2.one;
        else if (min == new Vector2(0f, 1f)) rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = position; rect.sizeDelta = size;
    }

    private static void AddOpeningScenesToBuildSettings()
    {
        var current = EditorBuildSettings.scenes.ToList();
        EditorBuildSettingsScene GetOrAdd(string path)
        {
            EditorBuildSettingsScene entry = current.FirstOrDefault(scene => scene.path == path);
            if (entry == null)
            {
                entry = new EditorBuildSettingsScene(path, true);
                current.Add(entry);
            }
            else entry.enabled = true;
            return entry;
        }

        EditorBuildSettingsScene login = GetOrAdd(LoginPath);
        EditorBuildSettingsScene map001 = GetOrAdd(Map001Path);
        EditorBuildSettingsScene map002 = GetOrAdd(ScenePath);
        EditorBuildSettingsScene[] ordered = new[] { login, map001, map002 }
            .Concat(current.Where(scene => scene.path != LoginPath && scene.path != Map001Path && scene.path != ScenePath))
            .ToArray();
        EditorBuildSettings.scenes = ordered;
    }
}
