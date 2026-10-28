using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public static class Map001AnimationImporter
{
    [Serializable] private sealed class AnimationData { public float fps; public int[] durationsMs; public bool loop; public AtlasFallback fallback; }
    [Serializable] private sealed class AtlasFallback { public int[] cellSize; public AtlasPage[] pages; }
    [Serializable] private sealed class AtlasPage { public string file; public int firstFrame; public int frameCount; public int columns; }

    public static void ImportAccepted()
    {
        ImportSet("AI/.Generated/NhanVat/NguoiChoiNamAnimationsBlue", "Assets/Art/NhanVat/Animations", 85f,
            new[] { "idle", "run", "jump", "hurt" }, true);
        ImportSet("AI/.Generated/NPC/AnhHungAnimationsBlue", "Assets/Art/NPC/Animations", 90f,
            new[] { "idle", "cast" }, false);
        ImportSet("AI/.Generated/Quai/BaQuaiLinhTruongAnimationsBlue", "Assets/Art/Quai/Animations", 115f,
            new[] { "idle", "attack", "jump", "hurt" }, false);
        AssetDatabase.Refresh();
        BuildController("Assets/Art/NhanVat/Animations/Player.controller", "Assets/Art/NhanVat/Animations", new[] { "idle", "run", "jump", "hurt" }, "player");
        BuildController("Assets/Art/NPC/Animations/Hero.controller", "Assets/Art/NPC/Animations", new[] { "idle", "cast" }, "hero");
        BuildController("Assets/Art/Quai/Animations/Gorilla.controller", "Assets/Art/Quai/Animations", new[] { "idle", "attack", "jump", "hurt" }, "gorilla");
        AssetDatabase.SaveAssets();
        Debug.Log("MAP-001 accepted animation packages imported and Animator Controllers generated.");
    }

    private static void ImportSet(string sourceRoot, string targetRoot, float ppu, string[] actions, bool playerSet)
    {
        EnsureFolder(targetRoot);
        foreach (string action in actions)
        {
            string statePath = Path.Combine(sourceRoot, "set_state.json").Replace('\\', '/');
            if (!File.Exists(statePath))
            {
                Debug.LogWarning("MAP-001 animation state file is missing for: " + action + " at " + statePath);
                continue;
            }
            if (!TryGetAcceptedTake(statePath, action, out int acceptedTake, out string acceptedPackage))
            {
                Debug.LogWarning("MAP-001 animation is not accepted; keeping it out of Unity: " + action);
                continue;
            }
            string source = string.IsNullOrEmpty(acceptedPackage)
                ? Path.Combine(sourceRoot, "actions", action, "takes", "t" + acceptedTake.ToString("D2"), "package")
                : Path.IsPathRooted(acceptedPackage) ? acceptedPackage : Path.Combine(sourceRoot, acceptedPackage);
            source = source.Replace('\\', '/');
            if (!File.Exists(Path.Combine(source, "animation.json")))
            {
                Debug.LogWarning("MAP-001 accepted animation package is missing: " + source);
                continue;
            }

            string target = targetRoot + "/" + action;
            EnsureFolder(target);
            foreach (string file in new[] { "animation.json", "animation-qa.json", "provenance.json" })
            {
                string inFile = Path.Combine(source, file).Replace('\\', '/');
                if (File.Exists(inFile)) File.Copy(inFile, target + "/" + file, true);
            }

            string jsonPath = target + "/animation.json";
            AnimationData data = JsonUtility.FromJson<AnimationData>(File.ReadAllText(jsonPath));
            if (data == null || data.fallback == null || data.fallback.pages == null || data.fallback.cellSize == null) continue;
            var frames = new List<Sprite>();
            for (int pageIndex = 0; pageIndex < data.fallback.pages.Length; pageIndex++)
            {
                AtlasPage page = data.fallback.pages[pageIndex];
                string sourceAtlas = Path.Combine(source, page.file).Replace('\\', '/');
                string targetAtlas = target + "/" + page.file;
                File.Copy(sourceAtlas, targetAtlas, true);
                AssetDatabase.ImportAsset(targetAtlas, ImportAssetOptions.ForceUpdate);
                ConfigureAtlas(targetAtlas, action, page, data.fallback.cellSize, ppu);
                Sprite[] imported = AssetDatabase.LoadAllAssetsAtPath(targetAtlas).OfType<Sprite>().ToArray();
                var byName = imported.ToDictionary(s => s.name, s => s);
                for (int n = 0; n < page.frameCount; n++)
                {
                    int globalIndex = page.firstFrame + n;
                    string spriteName = action + "_" + globalIndex.ToString("D3");
                    if (byName.TryGetValue(spriteName, out Sprite sprite)) frames.Add(sprite);
                }
            }
            if (frames.Count == 0) { Debug.LogError("No sprite frames loaded for " + target); continue; }
            CreateClip(target + "/" + action + ".anim", action, data, frames);
        }
    }

    private static bool TryGetAcceptedTake(string statePath, string action, out int take, out string packagePath)
    {
        take = 0;
        packagePath = null;
        string json = File.ReadAllText(statePath);
        int actionsIndex = json.IndexOf("\"actions\"", StringComparison.Ordinal);
        if (actionsIndex < 0) return false;
        int actionsOpen = json.IndexOf('{', actionsIndex);
        if (actionsOpen < 0) return false;
        int actionIndex = json.IndexOf("\"" + action + "\"", actionsOpen, StringComparison.Ordinal);
        if (actionIndex < 0) return false;
        int colon = json.IndexOf(':', actionIndex);
        int actionOpen = colon < 0 ? -1 : json.IndexOf('{', colon);
        int actionClose = FindMatchingBrace(json, actionOpen);
        if (actionOpen < 0 || actionClose < 0) return false;
        string block = json.Substring(actionOpen, actionClose - actionOpen + 1);
        int acceptedIndex = block.IndexOf("\"accepted\"", StringComparison.Ordinal);
        if (acceptedIndex < 0) return false;
        int acceptedColon = block.IndexOf(':', acceptedIndex);
        int acceptedOpen = acceptedColon < 0 ? -1 : block.IndexOf('{', acceptedColon);
        int acceptedClose = FindMatchingBrace(block, acceptedOpen);
        if (acceptedOpen < 0 || acceptedClose < 0) return false;
        string accepted = block.Substring(acceptedOpen, acceptedClose - acceptedOpen + 1);
        int takeIndex = accepted.IndexOf("\"take\"", StringComparison.Ordinal);
        int takeColon = takeIndex < 0 ? -1 : accepted.IndexOf(':', takeIndex);
        if (takeColon < 0) return false;
        int digit = takeColon + 1;
        while (digit < accepted.Length && char.IsWhiteSpace(accepted[digit])) digit++;
        int start = digit;
        while (digit < accepted.Length && char.IsDigit(accepted[digit])) digit++;
        if (digit <= start || !int.TryParse(accepted.Substring(start, digit - start), out take)) return false;

        // A manually reviewed window has a package-wSTART-END folder recorded in output.package.
        // Use that accepted path when present; otherwise use the take's standard package folder.
        int outputIndex = block.IndexOf("\"output\"", StringComparison.Ordinal);
        int outputColon = outputIndex < 0 ? -1 : block.IndexOf(':', outputIndex);
        int outputValue = outputColon < 0 ? -1 : outputColon + 1;
        while (outputValue >= 0 && outputValue < block.Length && char.IsWhiteSpace(block[outputValue])) outputValue++;
        if (outputValue >= 0 && outputValue < block.Length && block[outputValue] == '{')
        {
            int outputClose = FindMatchingBrace(block, outputValue);
            if (outputClose > outputValue)
            {
                string output = block.Substring(outputValue, outputClose - outputValue + 1);
                int packageIndex = output.IndexOf("\"package\"", StringComparison.Ordinal);
                int packageColon = packageIndex < 0 ? -1 : output.IndexOf(':', packageIndex);
                int quote = packageColon < 0 ? -1 : output.IndexOf('"', packageColon + 1);
                if (quote >= 0)
                {
                    int endQuote = quote + 1;
                    while (endQuote < output.Length)
                    {
                        if (output[endQuote] == '"' && output[endQuote - 1] != '\\') break;
                        endQuote++;
                    }
                    if (endQuote < output.Length) packagePath = output.Substring(quote + 1, endQuote - quote - 1);
                }
            }
        }
        return true;
    }

    private static int FindMatchingBrace(string json, int open)
    {
        if (open < 0 || open >= json.Length || json[open] != '{') return -1;
        int depth = 0;
        bool inString = false, escaped = false;
        for (int i = open; i < json.Length; i++)
        {
            char c = json[i];
            if (inString)
            {
                if (escaped) escaped = false;
                else if (c == '\\') escaped = true;
                else if (c == '"') inString = false;
                continue;
            }
            if (c == '"') inString = true;
            else if (c == '{') depth++;
            else if (c == '}' && --depth == 0) return i;
        }
        return -1;
    }

    private static void ConfigureAtlas(string atlasPath, string action, AtlasPage page, int[] cellSize, float ppu)
    {
        var ti = AssetImporter.GetAtPath(atlasPath) as TextureImporter;
        if (ti == null) return;
        ti.textureType = TextureImporterType.Sprite;
        ti.spriteImportMode = SpriteImportMode.Multiple;
        ti.spritePixelsPerUnit = ppu;
        ti.alphaIsTransparency = true;
        ti.mipmapEnabled = false;
        ti.isReadable = false;
        ti.filterMode = FilterMode.Bilinear;
        ti.SaveAndReimport();
        int columns = Math.Max(1, page.columns), cellW = cellSize[0], cellH = cellSize[1];
        TextureImporter importer = AssetImporter.GetAtPath(atlasPath) as TextureImporter;
        var factory = new SpriteDataProviderFactories();
        factory.Init();
        ISpriteEditorDataProvider dataProvider = factory.GetSpriteEditorDataProviderFromObject(importer);
        if (dataProvider == null) throw new InvalidOperationException("Sprite Editor data provider is unavailable for " + atlasPath);
        dataProvider.InitSpriteEditorDataProvider();

        var capabilityProvider = dataProvider.GetDataProvider<ISpriteFrameEditCapability>();
        if (capabilityProvider == null) throw new InvalidOperationException("Sprite frame edit capability is unavailable for " + atlasPath);
        var capability = capabilityProvider.GetEditCapability();
        if (!capability.HasCapability(EEditCapability.CreateAndDeleteSprite) ||
            !capability.HasCapability(EEditCapability.EditSpriteName) ||
            !capability.HasCapability(EEditCapability.EditSpriteRect) ||
            !capability.HasCapability(EEditCapability.EditPivot))
            throw new InvalidOperationException("Importer cannot safely create and edit sprite frames; atlas slicing stopped: " + atlasPath);

        var textureData = dataProvider.GetDataProvider<ITextureDataProvider>();
        if (textureData == null) throw new InvalidOperationException("Original texture dimensions are unavailable for " + atlasPath);
        textureData.GetTextureActualWidthAndHeight(out int sourceWidth, out int sourceHeight);
        var textureToSlice = global::Editor.SpriteEditorUtility.GetTextureToSlice(textureData);
        if (textureToSlice == null || textureToSlice.width != sourceWidth || textureToSlice.height != sourceHeight)
            throw new InvalidOperationException("Atlas texture size does not match original source; atlas slicing stopped: " + atlasPath);
        int height = textureToSlice.height;
        if (textureToSlice.hideFlags == HideFlags.HideAndDontSave) UnityEngine.Object.DestroyImmediate(textureToSlice);

        var existingRects = dataProvider.GetSpriteRects().ToDictionary(r => r.name, r => r);
        var spriteRects = new SpriteRect[page.frameCount];
        for (int i = 0; i < page.frameCount; i++)
        {
            int frame = page.firstFrame + i;
            int col = frame % columns, row = frame / columns;
            float x = col * cellW;
            float y = height - ((row + 1) * cellH);
            spriteRects[i] = new SpriteRect
            {
                name = action + "_" + frame.ToString("D3"),
                rect = new Rect(x, y, cellW, cellH),
                spriteID = existingRects.TryGetValue(action + "_" + frame.ToString("D3"), out SpriteRect oldRect)
                    ? oldRect.spriteID : UnityEditor.GUID.Generate(),
                alignment = SpriteAlignment.Custom,
                pivot = new Vector2(0.5f, 0f),
                border = Vector4.zero,
                customData = string.Empty
            };
        }

        var nameFileIdProvider = dataProvider.GetDataProvider<ISpriteNameFileIdDataProvider>();
        if (nameFileIdProvider == null) throw new InvalidOperationException("Sprite file-ID capability is unavailable for " + atlasPath);
        dataProvider.SetSpriteRects(spriteRects);
        nameFileIdProvider.SetNameFileIdPairs(spriteRects.Select(r => new SpriteNameFileIdPair(r.name, r.spriteID)));
        dataProvider.Apply();
        importer.SaveAndReimport();
    }

    public static void SliceGrid(string path, string prefix, int columns, int rows, float ppu)
    {
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null) throw new InvalidOperationException("Texture importer not found: " + path);
        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        importer.spritePixelsPerUnit = ppu;
        importer.alphaIsTransparency = true;
        importer.mipmapEnabled = false;
        importer.SaveAndReimport();
        importer = AssetImporter.GetAtPath(path) as TextureImporter;
        var factory = new SpriteDataProviderFactories();
        factory.Init();
        ISpriteEditorDataProvider dataProvider = factory.GetSpriteEditorDataProviderFromObject(importer);
        if (dataProvider == null) throw new InvalidOperationException("Sprite Editor data provider is unavailable for " + path);
        dataProvider.InitSpriteEditorDataProvider();
        var capabilityProvider = dataProvider.GetDataProvider<ISpriteFrameEditCapability>();
        if (capabilityProvider == null) throw new InvalidOperationException("Sprite frame edit capability is unavailable for " + path);
        var capability = capabilityProvider.GetEditCapability();
        if (!capability.HasCapability(EEditCapability.CreateAndDeleteSprite) ||
            !capability.HasCapability(EEditCapability.EditSpriteName) ||
            !capability.HasCapability(EEditCapability.EditSpriteRect) ||
            !capability.HasCapability(EEditCapability.EditPivot))
            throw new InvalidOperationException("Importer cannot safely slice this texture; slicing stopped: " + path);
        var textureData = dataProvider.GetDataProvider<ITextureDataProvider>();
        if (textureData == null) throw new InvalidOperationException("Original texture dimensions are unavailable for " + path);
        textureData.GetTextureActualWidthAndHeight(out int width, out int height);
        var readableTexture = global::Editor.SpriteEditorUtility.GetTextureToSlice(textureData);
        if (readableTexture == null || readableTexture.width != width || readableTexture.height != height)
            throw new InvalidOperationException("Texture size does not match original source; slicing stopped: " + path);
        if (readableTexture.hideFlags == HideFlags.HideAndDontSave) UnityEngine.Object.DestroyImmediate(readableTexture);
        var existingRects = dataProvider.GetSpriteRects().ToDictionary(r => r.name, r => r);
        if (existingRects.Count == columns * rows && Enumerable.Range(1, columns * rows).All(i => existingRects.ContainsKey(prefix + i.ToString("D2"))))
            return;
        float cellW = width / (float)columns, cellH = height / (float)rows;
        var spriteRects = new List<SpriteRect>();
        for (int y = 0; y < rows; y++)
        for (int x = 0; x < columns; x++)
        {
            spriteRects.Add(new SpriteRect
            {
                name = prefix + (y * columns + x + 1).ToString("D2"),
                rect = new Rect(x * cellW, height - ((y + 1) * cellH), cellW, cellH),
                spriteID = existingRects.TryGetValue(prefix + (y * columns + x + 1).ToString("D2"), out SpriteRect oldRect)
                    ? oldRect.spriteID : UnityEditor.GUID.Generate(),
                alignment = SpriteAlignment.Custom,
                pivot = new Vector2(0.5f, 0f),
                border = Vector4.zero,
                customData = string.Empty
            });
        }
        var nameFileIdProvider = dataProvider.GetDataProvider<ISpriteNameFileIdDataProvider>();
        if (nameFileIdProvider == null) throw new InvalidOperationException("Sprite file-ID capability is unavailable for " + path);
        dataProvider.SetSpriteRects(spriteRects.ToArray());
        nameFileIdProvider.SetNameFileIdPairs(spriteRects.Select(r => new SpriteNameFileIdPair(r.name, r.spriteID)));
        dataProvider.Apply();
        importer.SaveAndReimport();
    }

    private static void CreateClip(string path, string action, AnimationData data, List<Sprite> sprites)
    {
        AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
        if (clip != null) AssetDatabase.DeleteAsset(path);
        clip = new AnimationClip { name = action, frameRate = data.fps > 0f ? data.fps : 12f };
        var keys = new ObjectReferenceKeyframe[sprites.Count];
        float time = 0f;
        for (int i = 0; i < sprites.Count; i++)
        {
            keys[i] = new ObjectReferenceKeyframe { time = time, value = sprites[i] };
            int ms = data.durationsMs != null && i < data.durationsMs.Length ? data.durationsMs[i] : Mathf.RoundToInt(1000f / clip.frameRate);
            time += Mathf.Max(1, ms) / 1000f;
        }
        AnimationUtility.SetObjectReferenceCurve(clip, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"), keys);
        AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(clip);
        settings.loopTime = data.loop;
        AnimationUtility.SetAnimationClipSettings(clip, settings);
        AssetDatabase.CreateAsset(clip, path);
    }

    private static void BuildController(string path, string clipRoot, string[] actions, string kind)
    {
        if (File.Exists(path)) AssetDatabase.DeleteAsset(path);
        EnsureFolder(Path.GetDirectoryName(path).Replace('\\', '/'));
        AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath(path);
        var sm = controller.layers[0].stateMachine;
        var states = new Dictionary<string, AnimatorState>();
        foreach (string action in actions)
        {
            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(clipRoot + "/" + action + "/" + action + ".anim");
            if (clip == null) continue;
            AnimatorState state = sm.AddState(action);
            state.motion = clip;
            states[action] = state;
        }
        if (!states.TryGetValue("idle", out AnimatorState idle)) return;
        sm.defaultState = idle;

        if (kind == "player")
        {
            controller.AddParameter("Speed", AnimatorControllerParameterType.Float);
            controller.AddParameter("Jump", AnimatorControllerParameterType.Trigger);
            controller.AddParameter("Hurt", AnimatorControllerParameterType.Trigger);
            if (states.TryGetValue("run", out AnimatorState run))
            {
                AddTransition(idle, run, "Speed", AnimatorConditionMode.Greater, 0.08f, false);
                AddTransition(run, idle, "Speed", AnimatorConditionMode.Less, 0.08f, false);
            }
            AddAnyTrigger(sm, states, "jump", "Jump");
            AddAnyTrigger(sm, states, "hurt", "Hurt");
            AddReturn(states, "jump", idle);
            AddReturn(states, "hurt", idle);
        }
        else if (kind == "hero")
        {
            controller.AddParameter("Cast", AnimatorControllerParameterType.Trigger);
            AddTrigger(idle, states, "cast", "Cast");
            AddReturn(states, "cast", idle);
        }
        else
        {
            controller.AddParameter("Attack", AnimatorControllerParameterType.Trigger);
            controller.AddParameter("Jump", AnimatorControllerParameterType.Trigger);
            controller.AddParameter("Hurt", AnimatorControllerParameterType.Trigger);
            AddAnyTrigger(sm, states, "attack", "Attack");
            AddAnyTrigger(sm, states, "jump", "Jump");
            AddAnyTrigger(sm, states, "hurt", "Hurt");
            AddReturn(states, "attack", idle); AddReturn(states, "jump", idle); AddReturn(states, "hurt", idle);
        }
        EditorUtility.SetDirty(controller);
    }

    private static void AddAnyTrigger(AnimatorStateMachine sm, Dictionary<string, AnimatorState> states, string stateName, string trigger)
    {
        if (!states.TryGetValue(stateName, out AnimatorState state)) return;
        var t = sm.AddAnyStateTransition(state); t.hasExitTime = false; t.duration = 0.04f; t.AddCondition(AnimatorConditionMode.If, 0f, trigger);
    }

    private static void AddTrigger(AnimatorState from, Dictionary<string, AnimatorState> states, string stateName, string trigger)
    {
        if (!states.TryGetValue(stateName, out AnimatorState state)) return;
        var t = from.AddTransition(state); t.hasExitTime = false; t.duration = 0.04f; t.AddCondition(AnimatorConditionMode.If, 0f, trigger);
    }

    private static void AddTransition(AnimatorState from, AnimatorState to, string parameter, AnimatorConditionMode mode, float threshold, bool exit)
    {
        var t = from.AddTransition(to); t.hasExitTime = exit; t.duration = 0.06f; t.AddCondition(mode, threshold, parameter);
    }

    private static void AddReturn(Dictionary<string, AnimatorState> states, string fromName, AnimatorState idle)
    {
        if (!states.TryGetValue(fromName, out AnimatorState from)) return;
        var t = from.AddTransition(idle); t.hasExitTime = true; t.exitTime = 0.96f; t.duration = 0.06f;
    }

    private static void EnsureFolder(string path)
    {
        if (AssetDatabase.IsValidFolder(path)) return;
        string[] parts = path.Split('/'); string current = parts[0];
        for (int i = 1; i < parts.Length; i++)
        {
            string next = current + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next)) AssetDatabase.CreateFolder(current, parts[i]);
            current = next;
        }
    }
}
