using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Map001SceneBuilder
{
    private const string Art = "Assets/Art/Map/ThungLungHoang/";
    private const string ScenePath = "Assets/Scenes/ThungLungHoang.unity";
    private static Sprite whiteSprite;
    private static Sprite groundTexture;
    private static Sprite forestSprite;
    private static Sprite[] trees;
    private static Sprite playerSprite;
    private static Sprite heroSprite;
    private static Sprite gorillaSprite;
    private static Sprite iceSprite;
    private static Material lineMaterial;

    public static void Build()
    {
        AssetDatabase.Refresh();
        EnsureSpriteImport(Art + "RungXaThungLungHoang.png", 100f, false);
        EnsureSpriteImport(Art + "BoNenDatDa.png", 100f, true);
        EnsureSpriteImport(Art + "HieuUngBangDong.png", 100f, false);
        EnsureSpriteImport("Assets/Art/NhanVat/NguoiChoiNam.png", 100f, false);
        EnsureSpriteImport("Assets/Art/NPC/AnhHung.png", 100f, false);
        EnsureSpriteImport("Assets/Art/Quai/BaQuaiLinhTruong.png", 100f, false);
        AssetDatabase.Refresh();

        whiteSprite = CreateWhiteSpriteAsset(Art + "MauTrang.png");
        forestSprite = AssetDatabase.LoadAssetAtPath<Sprite>(Art + "RungXaThungLungHoang.png");
        groundTexture = AssetDatabase.LoadAssetAtPath<Sprite>(Art + "BoNenDatDa.png");
        iceSprite = CropSprite("Assets/Art/Map/ThungLungHoang/HieuUngBangDong.png", Art + "BangDongSprite.asset", 1f);
        playerSprite = CropSprite("Assets/Art/NhanVat/NguoiChoiNam.png", "Assets/Art/NhanVat/NguoiChoiNamSprite.asset", 3f);
        heroSprite = CropSprite("Assets/Art/NPC/AnhHung.png", "Assets/Art/NPC/AnhHungSprite.asset", 3f);
        gorillaSprite = CropSprite("Assets/Art/Quai/BaQuaiLinhTruong.png", "Assets/Art/Quai/BaQuaiLinhTruongSprite.asset", 2.2f);
        trees = CreateTreeSlices();

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        RenderSettings.ambientLight = new Color(0.28f, 0.31f, 0.27f);
        BuildCamera(scene);
        BuildMapGeometry();
        BuildBackground();
        BuildMiddleTrees();
        BuildExit();
        BuildActors();
        BuildHub();
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene, ScenePath);
        AssetDatabase.SaveAssets();
        Debug.Log("MAP-001 Thung Lung Hoang created at " + ScenePath);
    }

    private static void EnsureSpriteImport(string path, float ppu, bool tiled)
    {
        var ti = AssetImporter.GetAtPath(path) as TextureImporter;
        if (ti == null) return;
        ti.textureType = TextureImporterType.Sprite;
        ti.spriteImportMode = SpriteImportMode.Single;
        ti.spritePixelsPerUnit = ppu;
        ti.alphaIsTransparency = true;
        ti.mipmapEnabled = false;
        ti.filterMode = FilterMode.Bilinear;
        ti.wrapMode = tiled ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
        ti.spritePivot = new Vector2(0.5f, 0f);
        ti.SaveAndReimport();
    }

    private static Sprite CreateWhiteSpriteAsset(string path)
    {
        if (!File.Exists(path))
        {
            var tex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            var pixels = new Color32[16];
            for (int i = 0; i < pixels.Length; i++) pixels[i] = new Color32(255, 255, 255, 255);
            tex.SetPixels32(pixels);
            tex.Apply();
            File.WriteAllBytes(path, tex.EncodeToPNG());
            UnityEngine.Object.DestroyImmediate(tex);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
        EnsureSpriteImport(path, 4f, false);
        return AssetDatabase.LoadAssetAtPath<Sprite>(path);
    }

    private static Sprite CropSprite(string texturePath, string assetPath, float targetHeight)
    {
        Sprite existing = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
        if (existing != null) return existing;
        var ti = AssetImporter.GetAtPath(texturePath) as TextureImporter;
        if (ti != null && !ti.isReadable) { ti.isReadable = true; ti.SaveAndReimport(); }
        var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
        if (texture == null) return null;
        Color32[] pixels = texture.GetPixels32();
        int minX = texture.width, minY = texture.height, maxX = -1, maxY = -1;
        for (int y = 0; y < texture.height; y++)
        for (int x = 0; x < texture.width; x++)
        {
            if (pixels[y * texture.width + x].a <= 8) continue;
            minX = Math.Min(minX, x); minY = Math.Min(minY, y);
            maxX = Math.Max(maxX, x); maxY = Math.Max(maxY, y);
        }
        if (maxX < minX || maxY < minY) return null;
        var rect = new Rect(minX, minY, maxX - minX + 1, maxY - minY + 1);
        float ppu = rect.height / Mathf.Max(0.1f, targetHeight);
        Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0f), ppu, 0, SpriteMeshType.Tight);
        sprite.name = Path.GetFileNameWithoutExtension(assetPath);
        EnsureAssetFolder(Path.GetDirectoryName(assetPath).Replace('\\', '/'));
        AssetDatabase.CreateAsset(sprite, assetPath);
        return sprite;
    }

    private static Sprite[] CreateTreeSlices()
    {
        string texturePath = Art + "CayRungThungLungHoang.png";
        Map001AnimationImporter.SliceGrid(texturePath, "CayRung", 3, 1, 100f);
        return AssetDatabase.LoadAllAssetsAtPath(texturePath)
            .OfType<Sprite>()
            .Where(s => s.name.StartsWith("CayRung", StringComparison.Ordinal))
            .OrderBy(s => s.name)
            .ToArray();
    }

    private static void BuildCamera(Scene scene)
    {
        GameObject cameraObject = new GameObject("Camera Gameplay", typeof(Camera), typeof(AudioListener), typeof(Map001CameraRig));
        cameraObject.tag = "MainCamera";
        cameraObject.transform.position = new Vector3(8f, 4.5f, -10f);
        var camera = cameraObject.GetComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = 4.5f;
        camera.backgroundColor = new Color(0.035f, 0.055f, 0.05f, 1f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        cameraObject.GetComponent<Map001CameraRig>().mapWidth = 96f;
        cameraObject.GetComponent<Map001CameraRig>().mapHeight = 18f;
    }

    private static readonly float[,] Platforms =
    {
        {0f, 16.8f, 2.0f}, {17.2f, 28.5f, 2.55f}, {29.45f, 41.7f, 1.75f},
        {42.6f, 56.0f, 2.5f}, {56.9f, 68.3f, 2.5f}, {69.2f, 80.8f, 3.15f},
        {81.8f, 91.4f, 3.15f}, {92.3f, 96f, 4.0f}
    };

    private static void BuildMapGeometry()
    {
        GameObject root = new GameObject("Địa hình chơi — khung MAP-001");
        var pathPoints = new System.Collections.Generic.List<Vector3>();
        for (int i = 0; i < Platforms.GetLength(0); i++)
        {
            float start = Platforms[i, 0], end = Platforms[i, 1], top = Platforms[i, 2];
            float width = end - start;
            GameObject segment = new GameObject("Nền " + (i + 1).ToString("00") + " — chữ nhật ghép");
            segment.transform.SetParent(root.transform, false);
            segment.transform.position = new Vector3(start + width * 0.5f, top - 1.65f, 0f);
            var sr = segment.AddComponent<SpriteRenderer>();
            sr.sprite = groundTexture;
            sr.drawMode = SpriteDrawMode.Tiled;
            sr.size = new Vector2(width, 3.3f);
            sr.sortingOrder = 0;
            var collider = segment.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(width, 3.3f);
            collider.sharedMaterial = new PhysicsMaterial2D("Đất ít trượt") { friction = 0.85f, bounciness = 0f };

            GameObject lip = new GameObject("Mặt cỏ");
            lip.transform.SetParent(root.transform, false);
            lip.transform.position = new Vector3(start + width * 0.5f, top + 0.015f, -0.05f);
            lip.transform.localScale = new Vector3(width, 0.07f, 1f);
            var grass = lip.AddComponent<SpriteRenderer>();
            grass.sprite = whiteSprite;
            grass.color = new Color(0.25f, 0.33f, 0.15f, 1f);
            grass.sortingOrder = 1;

            pathPoints.Add(new Vector3(start + 0.08f, top + 0.105f, -0.1f));
            pathPoints.Add(new Vector3(end - 0.08f, top + 0.105f, -0.1f));
        }
        AddWorldBounds(root.transform);
        BuildGoldTrail(root.transform, pathPoints);
        BuildGrassTufts(root.transform);
    }

    private static void AddWorldBounds(Transform parent)
    {
        MakeBoundary(parent, "Ranh trái", new Vector2(0.25f, 9f), new Vector2(0.5f, 18f));
        MakeBoundary(parent, "Ranh phải", new Vector2(95.75f, 9f), new Vector2(0.5f, 18f));
        MakeBoundary(parent, "Ranh dưới", new Vector2(48f, -0.8f), new Vector2(96f, 0.6f));
        MakeBoundary(parent, "Ranh trên", new Vector2(48f, 18.3f), new Vector2(96f, 0.5f));
    }

    private static void MakeBoundary(Transform parent, string name, Vector2 center, Vector2 size)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        go.transform.position = center;
        go.AddComponent<BoxCollider2D>().size = size;
    }

    private static void BuildGoldTrail(Transform parent, System.Collections.Generic.List<Vector3> points)
    {
        // Keep each platform's guide separate so the trail never bridges a gap or a height change.
        for (int i = 0; i + 1 < points.Count; i += 2)
        {
            Vector3[] pair = { points[i], points[i + 1] };
            GameObject glowGo = new GameObject("Đường bụi vàng — viền sáng " + (i / 2 + 1).ToString("00"));
            glowGo.transform.SetParent(parent, false);
            var glow = glowGo.AddComponent<LineRenderer>();
            glow.useWorldSpace = true; glow.positionCount = 2; glow.SetPositions(pair);
            glow.startWidth = 0.11f; glow.endWidth = 0.09f; glow.numCornerVertices = 3; glow.numCapVertices = 3;
            glow.material = GetLineMaterial(); glow.startColor = new Color(0.88f, 0.63f, 0.21f, 0.16f); glow.endColor = glow.startColor; glow.sortingOrder = 3;
            GameObject lineGo = new GameObject("Dấu vàng — lối thoát " + (i / 2 + 1).ToString("00"));
            lineGo.transform.SetParent(parent, false);
            var line = lineGo.AddComponent<LineRenderer>();
            line.useWorldSpace = true; line.positionCount = 2; line.SetPositions(pair);
            line.startWidth = 0.035f; line.endWidth = 0.03f; line.numCornerVertices = 2; line.numCapVertices = 2;
            line.material = GetLineMaterial(); line.startColor = new Color(0.88f, 0.68f, 0.3f, 0.82f); line.endColor = line.startColor; line.sortingOrder = 4;
        }
    }

    private static void BuildGrassTufts(Transform parent)
    {
        float[] xs = {3.2f, 8.7f, 13.5f, 20.2f, 25.1f, 32.8f, 38.1f, 45.2f, 52.7f, 60.3f, 65.2f, 73.7f, 78.3f, 86.4f, 90.2f};
        for (int i = 0; i < xs.Length; i++)
        {
            float y = GroundY(xs[i]) + 0.12f;
            for (int blade = 0; blade < 3; blade++)
            {
                float x = xs[i] + (blade - 1) * 0.1f;
                GameObject tuft = new GameObject("Cỏ tiền cảnh " + i + "-" + blade);
                tuft.transform.SetParent(parent, false);
                var lr = tuft.AddComponent<LineRenderer>();
                lr.useWorldSpace = true; lr.positionCount = 2; lr.startWidth = 0.035f; lr.endWidth = 0.012f;
                lr.SetPosition(0, new Vector3(x, y, -0.3f));
                lr.SetPosition(1, new Vector3(x + (blade - 1) * 0.09f, y + 0.23f + 0.05f * ((i + blade) % 2), -0.3f));
                lr.material = GetLineMaterial();
                lr.startColor = new Color(0.61f, 0.67f, 0.31f, 0.85f); lr.endColor = lr.startColor; lr.sortingOrder = 12;
            }
        }
    }

    private static Material GetLineMaterial()
    {
        if (lineMaterial == null)
        {
            lineMaterial = new Material(Shader.Find("Sprites/Default"));
            lineMaterial.name = "MAP-001 — nét 2D";
        }
        return lineMaterial;
    }

    private static float GroundY(float x)
    {
        for (int i = 0; i < Platforms.GetLength(0); i++)
            if (x >= Platforms[i, 0] && x <= Platforms[i, 1]) return Platforms[i, 2];
        return 2f;
    }

    private static void BuildBackground()
    {
        Camera cam = Camera.main;
        CreateForestLayer("Rừng xa — nền", cam.transform, 0.14f, 1f, 5.4f, new Color(1f, 1f, 1f, 1f), -30, 5, -0.4f);
        CreateForestLayer("Tán rừng — lớp giữa", cam.transform, 0.3f, 1.02f, 5.1f, new Color(0.72f, 0.78f, 0.74f, 0.52f), -25, 5, 1.1f);
    }

    private static void CreateForestLayer(string name, Transform camera, float factor, float scale, float y, Color color, int order, int count, float xOffset)
    {
        var root = new GameObject(name);
        var parallax = root.AddComponent<Map001Parallax>();
        parallax.cameraTransform = camera; parallax.factor = factor;
        float tileWidth = forestSprite.rect.width / forestSprite.pixelsPerUnit * scale;
        for (int i = 0; i < count; i++)
        {
            GameObject tile = new GameObject("Mảng rừng " + (i + 1));
            tile.transform.SetParent(root.transform, false);
            tile.transform.localPosition = new Vector3(tileWidth * (i + 0.5f) + xOffset, y, 2f);
            tile.transform.localScale = new Vector3(scale, scale, 1f);
            var sr = tile.AddComponent<SpriteRenderer>(); sr.sprite = forestSprite; sr.color = color; sr.sortingOrder = order;
        }
    }

    private static void BuildMiddleTrees()
    {
        if (trees == null || trees.Length == 0) return;
        float[] xs = {5.4f, 11.5f, 18.2f, 25.6f, 34.0f, 40.5f, 50.2f, 58.8f, 65.7f, 75.2f, 83.2f, 89.3f};
        for (int i = 0; i < xs.Length; i++)
        {
            Sprite sprite = trees[i % trees.Length];
            var go = new GameObject("Cây trung cảnh " + (i + 1).ToString("00"));
            go.transform.position = new Vector3(xs[i], GroundY(xs[i]) - 0.02f, 0.4f);
            go.transform.localScale = Vector3.one * (i % 3 == 0 ? 0.58f : 0.49f);
            var sr = go.AddComponent<SpriteRenderer>(); sr.sprite = sprite; sr.sortingOrder = -4;
            if (i % 2 == 0) go.AddComponent<Map001Float>().height = 0.008f;
        }
    }

    private static void BuildActors()
    {
        Camera camera = Camera.main;
        GameObject player = new GameObject("Người chơi — nam lữ khách");
        player.transform.position = new Vector3(3.4f, GroundY(3.4f) + 0.02f, 0f);
        var playerSr = player.AddComponent<SpriteRenderer>(); playerSr.sprite = playerSprite; playerSr.sortingOrder = 10;
        var rb = player.AddComponent<Rigidbody2D>(); rb.gravityScale = 4.2f; rb.mass = 1.1f;
        var box = player.AddComponent<BoxCollider2D>(); box.size = new Vector2(0.78f, 1.95f); box.offset = new Vector2(0f, 0.98f);
        Animator playerAnimator = player.AddComponent<Animator>();
        playerAnimator.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Art/NhanVat/Animations/Player.controller");
        player.AddComponent<Map001PlayerController>();
        var cameraRig = camera.GetComponent<Map001CameraRig>(); cameraRig.target = player.transform;

        GameObject hero = new GameObject("Anh Hùng — điểm chặn quái");
        hero.transform.position = new Vector3(48.5f, GroundY(48.5f) + 0.1f, 0f);
        var heroSr = hero.AddComponent<SpriteRenderer>(); heroSr.sprite = heroSprite; heroSr.sortingOrder = 9;
        Animator heroAnim = hero.AddComponent<Animator>();
        heroAnim.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Art/NPC/Animations/Hero.controller");
        hero.AddComponent<Map001Float>(); hero.SetActive(false);

        var enemies = new Map001EnemyController[3];
        float[] enemyX = {75.3f, 78.4f, 81.1f};
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = new GameObject("Khỉ Đột " + (i + 1).ToString("00"));
            enemy.transform.position = new Vector3(enemyX[i], GroundY(enemyX[i]) + 0.02f, 0f);
            enemy.transform.localScale = new Vector3(-1.15f, 1.15f, 1f);
            var sr = enemy.AddComponent<SpriteRenderer>(); sr.sprite = gorillaSprite; sr.sortingOrder = 11;
            Animator enemyAnimator = enemy.AddComponent<Animator>();
            enemyAnimator.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Art/Quai/Animations/Gorilla.controller");
            var enemyBody = enemy.AddComponent<Rigidbody2D>(); enemyBody.gravityScale = 4f; enemyBody.mass = 2f;
            var enemyBox = enemy.AddComponent<BoxCollider2D>(); enemyBox.size = new Vector2(1.2f, 1.55f); enemyBox.offset = new Vector2(0f, 0.78f);
            enemies[i] = enemy.AddComponent<Map001EnemyController>(); enemies[i].player = player.GetComponent<Map001PlayerController>();
            enemy.SetActive(false);
        }

        GameObject ice = new GameObject("Hiệu ứng đóng băng");
        ice.transform.position = new Vector3(49.4f, GroundY(49f) + 1.0f, -0.25f);
        ice.transform.localScale = Vector3.one * 1.3f;
        var iceSr = ice.AddComponent<SpriteRenderer>(); iceSr.sprite = iceSprite; iceSr.color = new Color(0.74f, 0.9f, 1f, 0.82f); iceSr.sortingOrder = 15;
        ice.SetActive(false);

        GameObject director = new GameObject("Diễn biến MAP-001");
        var runtime = director.AddComponent<Map001RuntimeDirector>();
        runtime.player = player.GetComponent<Map001PlayerController>(); runtime.enemies = enemies;
        runtime.hero = hero; runtime.heroAnimator = heroAnim; runtime.iceEffect = ice;
        runtime.hud = UnityEngine.Object.FindObjectOfType<Map001Hud>();
        runtime.exitPoint = GameObject.Find("Cổng sang Khu Đất Sau Nhà").transform;
    }

    private static void BuildExit()
    {
        GameObject exit = new GameObject("Cổng sang Khu Đất Sau Nhà");
        exit.transform.position = new Vector3(95.1f, GroundY(94f) + 2.0f, 0f);
        var ring = exit.AddComponent<LineRenderer>(); ring.useWorldSpace = false; ring.loop = true; ring.positionCount = 24;
        for (int i = 0; i < ring.positionCount; i++)
        {
            float a = i * Mathf.PI * 2f / ring.positionCount;
            ring.SetPosition(i, new Vector3(Mathf.Cos(a) * 0.56f, Mathf.Sin(a) * 0.9f, 0f));
        }
        ring.startWidth = 0.08f; ring.endWidth = 0.08f; ring.material = new Material(Shader.Find("Sprites/Default"));
        ring.startColor = new Color(1f, 0.79f, 0.26f, 0.95f); ring.endColor = ring.startColor; ring.sortingOrder = 7;
        var trigger = exit.AddComponent<BoxCollider2D>(); trigger.isTrigger = true; trigger.size = new Vector2(2.2f, 3.4f);
        GameObject marker = new GameObject("Bảng tên khu vực kế tiếp"); marker.transform.SetParent(exit.transform, false); marker.transform.localPosition = new Vector3(-0.5f, 1.25f, 0f);
        var canvas = marker.AddComponent<Canvas>(); canvas.renderMode = RenderMode.WorldSpace; canvas.worldCamera = Camera.main;
        var rect = marker.GetComponent<RectTransform>(); rect.sizeDelta = new Vector2(430f, 78f); marker.transform.localScale = Vector3.one * 0.008f;
        var panel = marker.AddComponent<Image>(); panel.color = new Color(0.045f, 0.09f, 0.085f, 0.94f);
        GameObject label = CreateUiText("Tên khu vực", rect, "KHU ĐẤT SAU NHÀ", 25, TextAnchor.MiddleCenter, Color.white);
        Stretch(label.GetComponent<RectTransform>(), 12f, 3f, -12f, -3f);
        marker.SetActive(false);
        var hud = UnityEngine.Object.FindObjectOfType<Map001Hud>();
        // The hint is also available in the Hub; the world sign remains hidden until the player approaches.
        if (hud != null) hud.exitHint = marker;
    }

    private static void BuildHub()
    {
        GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
        GameObject canvasObject = new GameObject("UI HUB", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvas = canvasObject.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 100;
        CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f); scaler.matchWidthOrHeight = 0.5f;
        Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        GameObject plate = CreatePanel("Bảng sinh lực", canvas.transform, new Color(0.045f, 0.085f, 0.078f, 0.88f));
        Anchor(plate.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(40f, -40f), new Vector2(500f, 148f));
        CreateText("Tên map", plate.transform, "THUNG LŨNG HOANG", 27, TextAnchor.MiddleLeft, new Color(0.95f, 0.83f, 0.52f), font, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(22f, -10f), new Vector2(-22f, -62f));
        Text healthText = CreateText("Sinh lực", plate.transform, "Sinh lực  100 / 100", 20, TextAnchor.MiddleLeft, Color.white, font, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(24f, -61f), new Vector2(-24f, -88f));
        GameObject healthBack = CreatePanel("Nền thanh sinh lực", plate.transform, new Color(0.16f, 0.12f, 0.12f, 1f));
        Anchor(healthBack.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(24f, -100f), new Vector2(-24f, -124f));
        GameObject healthFillGo = CreatePanel("Thanh sinh lực", healthBack.transform, new Color(0.75f, 0.2f, 0.16f, 1f));
        Anchor(healthFillGo.GetComponent<RectTransform>(), new Vector2(0f, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
        Image healthFill = healthFillGo.GetComponent<Image>(); healthFill.type = Image.Type.Filled; healthFill.fillMethod = Image.FillMethod.Horizontal;

        GameObject objective = CreatePanel("Mục tiêu", canvas.transform, new Color(0.045f, 0.085f, 0.078f, 0.78f));
        Anchor(objective.GetComponent<RectTransform>(), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(40f, -202f), new Vector2(500f, 66f));
        Text objectiveText = CreateText("Nhiệm vụ", objective.transform, "Bám theo dấu vàng đến lối ra", 19, TextAnchor.MiddleLeft, new Color(0.95f, 0.92f, 0.83f), font, new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(22f, 0f), new Vector2(-16f, 0f));

        GameObject hubLabel = CreatePanel("Nhận diện Hub", canvas.transform, new Color(0.045f, 0.085f, 0.078f, 0.75f));
        Anchor(hubLabel.GetComponent<RectTransform>(), new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(-40f, -40f), new Vector2(270f, 58f));
        CreateText("Trạng thái", hubLabel.transform, "HUB  •  ĐANG Ở TRONG GAME", 16, TextAnchor.MiddleCenter, new Color(0.89f, 0.91f, 0.82f), font, Vector2.zero, Vector2.one, new Vector2(10f, 6f), new Vector2(-10f, -6f));

        GameObject buttons = CreatePanel("Thanh công cụ Hub", canvas.transform, new Color(0.045f, 0.085f, 0.078f, 0.82f));
        Anchor(buttons.GetComponent<RectTransform>(), new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-40f, 40f), new Vector2(496f, 84f));
        string[] names = {"Túi đồ", "Nhân vật", "Nhiệm vụ"};
        for (int i = 0; i < names.Length; i++)
        {
            GameObject b = CreatePanel("Nút " + names[i], buttons.transform, new Color(0.19f, 0.28f, 0.23f, 0.96f));
            Anchor(b.GetComponent<RectTransform>(), new Vector2(0f, 0f), new Vector2(0f, 1f), new Vector2(14f + i * 158f, 12f), new Vector2(148f, -12f));
            CreateText("Nhãn", b.transform, names[i], 17, TextAnchor.MiddleCenter, Color.white, font, Vector2.zero, Vector2.one, new Vector2(6f, 2f), new Vector2(-6f, -2f));
            b.AddComponent<Button>();
        }

        GameObject messagePanel = CreatePanel("Lời thoại", canvas.transform, new Color(0.035f, 0.075f, 0.09f, 0.92f));
        Anchor(messagePanel.GetComponent<RectTransform>(), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -46f), new Vector2(660f, 70f));
        Text message = CreateText("Lời Anh Hùng", messagePanel.transform, "", 25, TextAnchor.MiddleCenter, new Color(0.92f, 0.96f, 1f), font, Vector2.zero, Vector2.one, new Vector2(12f, 4f), new Vector2(-12f, -4f));
        messagePanel.SetActive(false);

        GameObject completion = CreatePanel("Hoàn thành map", canvas.transform, new Color(0.025f, 0.07f, 0.065f, 0.96f));
        Anchor(completion.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(560f, 240f));
        Text completionText = CreateText("Bảng hoàn thành", completion.transform, "Đường đã thông\nKhu Đất Sau Nhà", 32, TextAnchor.MiddleCenter, new Color(0.96f, 0.84f, 0.5f), font, Vector2.zero, Vector2.one, new Vector2(20f, 10f), new Vector2(-20f, -10f));
        completion.SetActive(false);

        var hud = canvasObject.AddComponent<Map001Hud>();
        hud.healthText = healthText; hud.healthFill = healthFill; hud.objectiveText = objectiveText;
        hud.messageText = message; hud.messagePanel = messagePanel; hud.completionPanel = completion; hud.completionText = completionText;
        hud.exitHint = GameObject.Find("Bảng tên khu vực kế tiếp");
        Map001RuntimeDirector director = UnityEngine.Object.FindObjectOfType<Map001RuntimeDirector>();
        hud.player = UnityEngine.Object.FindObjectOfType<Map001PlayerController>();
        if (director != null) director.hud = hud;
    }

    private static GameObject CreatePanel(string name, Transform parent, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        go.transform.SetParent(parent, false); go.GetComponent<Image>().color = color;
        return go;
    }

    private static GameObject CreateUiText(string name, Transform parent, string value, int size, TextAnchor align, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        go.transform.SetParent(parent, false);
        var text = go.GetComponent<Text>(); text.text = value; text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = size; text.alignment = align; text.color = color; text.raycastTarget = false; text.horizontalOverflow = HorizontalWrapMode.Wrap; text.verticalOverflow = VerticalWrapMode.Overflow;
        return go;
    }

    private static Text CreateText(string name, Transform parent, string value, int size, TextAnchor align, Color color, Font font, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
    {
        GameObject go = CreateUiText(name, parent, value, size, align, color);
        var rt = go.GetComponent<RectTransform>(); rt.anchorMin = anchorMin; rt.anchorMax = anchorMax; rt.offsetMin = offsetMin; rt.offsetMax = offsetMax;
        go.GetComponent<Text>().font = font; return go.GetComponent<Text>();
    }

    private static void Anchor(RectTransform rect, Vector2 min, Vector2 max, Vector2 anchoredPosition, Vector2 sizeDelta)
    {
        rect.anchorMin = min; rect.anchorMax = max; rect.pivot = min;
        if (min == new Vector2(1f, 1f)) rect.pivot = new Vector2(1f, 1f);
        if (min == new Vector2(1f, 0f)) rect.pivot = new Vector2(1f, 0f);
        if (min == new Vector2(0.5f, 1f)) rect.pivot = new Vector2(0.5f, 1f);
        if (min == new Vector2(0.5f, 0.5f)) rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = anchoredPosition; rect.sizeDelta = sizeDelta;
    }

    private static void Stretch(RectTransform rect, float left, float bottom, float right, float top)
    {
        rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = new Vector2(left, bottom); rect.offsetMax = new Vector2(right, top);
    }

    private static void EnsureAssetFolder(string path)
    {
        string[] parts = path.Split('/'); string current = parts[0];
        for (int i = 1; i < parts.Length; i++)
        {
            string next = current + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next)) AssetDatabase.CreateFolder(current, parts[i]);
            current = next;
        }
    }
}
