using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RenderTextureExtension
{
    public static Texture2D ToTexture2D(this RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.Alpha8, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}

public class DestructibleTrunkFaceRenderer : MonoBehaviour
{
    public RenderTexture renderTexture;

    public Material brushMaterial;

    public RenderTexture GetRenderTexture()
    {
        if (!renderTexture)
        {
            renderTexture = new RenderTexture(1024, 1024, 0);

            // fill texture with WHITE 
            var rt = RenderTexture.active;
            RenderTexture.active = renderTexture;
            GL.Clear(false, true, Color.white);
            RenderTexture.active = rt;
        }

        return renderTexture;
    }

    private void Awake()
    {
    
    }

    private void OnDestroy()
    {
        if (renderTexture && renderTexture.IsCreated())
        {
            renderTexture.Release();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            byte[] bytes = renderTexture.ToTexture2D().EncodeToPNG();
            System.IO.File.WriteAllBytes("F:/SavedScreen.bin", bytes);

            bytes = renderTexture.ToTexture2D().GetRawTextureData();
        }
    }

    public void DrawBrushInTrunkSpace(Mesh mesh, Vector2 offset, Vector2 trunkSize, ClipType clipType)
    {
        int passIdx;
        if (clipType == ClipType.Sub)
            passIdx = 0;
        else
            passIdx = 2;
            
        var rt = RenderTexture.active;
        RenderTexture.active = renderTexture;

        brushMaterial.SetVector("_ScaleOffset", new Vector4(offset.x, offset.y, trunkSize.x, trunkSize.y));

        // base pass
        brushMaterial.SetPass(passIdx);
        Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
        // outline pass
        brushMaterial.SetPass(passIdx + 1);
        Graphics.DrawMeshNow(mesh, Matrix4x4.identity);

        RenderTexture.active = rt;
    }
}

