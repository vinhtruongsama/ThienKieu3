using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Editor
{
    public static partial class SpriteEditorUtility
    {
        public static Texture2D GetTextureToSlice(ITextureDataProvider textureDataProvider)
        {
            textureDataProvider.GetTextureActualWidthAndHeight(out int width, out int height);
            Texture2D readableTexture = textureDataProvider.GetReadableTexture2D();
            if (readableTexture == null || (readableTexture.width == width && readableTexture.height == height))
                return readableTexture;
            if (!ShaderUtil.hardwareSupportsRectRenderTexture || readableTexture == null) return null;
            RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR));
            RenderTexture previous = RenderTexture.active;
            Graphics.Blit(readableTexture, temporary);
            RenderTexture.active = temporary;
            bool mipmaps = width >= SystemInfo.maxTextureSize || height >= SystemInfo.maxTextureSize;
            Texture2D duplicate = new Texture2D(width, height, TextureFormat.RGBA32, mipmaps);
            duplicate.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
            duplicate.Apply();
            duplicate.alphaIsTransparency = readableTexture.alphaIsTransparency;
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(temporary);
            duplicate.hideFlags = HideFlags.HideAndDontSave;
            return duplicate;
        }
    }
}
