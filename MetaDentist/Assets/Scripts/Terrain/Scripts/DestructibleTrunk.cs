using System.Collections.Generic;
using UnityEngine;
using Vector2f = UnityEngine.Vector2;
using Vector2i = ClipperLib.IntPoint;

using int64 = System.Int64;

public class DestructibleTrunk : MonoBehaviour, IDestructible
{
    public Material faceMaterial;

    public Material edgeMaterial;

    [Range(0.25f, 1.0f)]
    public float blockSize;

    private int64 blockSizeScaled;

    [Range(5, 100)]
    public int resolutionX = 10;

    [Range(5, 100)]
    public int resolutionY = 10;

    public float depth = 1.0f;

    private float width;

    private float height;

    private Mesh faceMesh;

    private Mesh edgeMesh;

    private DestructibleBlock[] blocks;

    private DestructibleTrunkFaceRenderer renderer;

    private void Awake()
    {
        DestructibleTerrainManager.Instance.AddSubject(this);

        width = blockSize * resolutionX;
        height = blockSize * resolutionY;
        blockSizeScaled = (int64)(blockSize * VectorEx.float2int64);
        renderer = GetComponent<DestructibleTrunkFaceRenderer>();     

        Initialize();
    }

    public void Initialize()
    {
        blocks = new DestructibleBlock[resolutionX * resolutionY];

        GameObject faceVisual = new GameObject();
        faceVisual.transform.SetParent(transform);
        faceVisual.transform.localPosition = Vector3.zero;
        faceVisual.name = "FaceVisual";

        faceMesh = new Mesh();
        faceMesh.vertices = new Vector3[]{
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, height, 0f),
            new Vector3(width, height, 0f),
            new Vector3(width, 0f, 0f) 
        };
        faceMesh.uv = new Vector2[]{
            new Vector2f(0f, 0f),
            new Vector2f(0f, 1f),
            new Vector2f(1f, 1f),
            new Vector2f(1f, 0f)
        };
        faceMesh.normals = new Vector3[]{
            new Vector3(0f, 0f, -1f),
            new Vector3(0f, 0f, -1f),
            new Vector3(0f, 0f, -1f),
            new Vector3(0f, 0f, -1f)
        };
        faceMesh.triangles = new int[]{
            0, 1, 3,
            3, 1, 2
        };

        MeshFilter meshFilter = faceVisual.AddComponent<MeshFilter>();
        meshFilter.mesh = faceMesh;

        MeshRenderer meshRenderer = faceVisual.AddComponent<MeshRenderer>();
        meshRenderer.material = faceMaterial;
        meshRenderer.material.SetTexture("_MaskTex", renderer.GetRenderTexture());
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        GameObject edgeVisual = new GameObject();
        edgeVisual.transform.SetParent(transform);
        edgeVisual.transform.localPosition = Vector3.zero;
        edgeVisual.name = "EdgeVisual";

        edgeMesh = new Mesh();
        meshFilter = edgeVisual.AddComponent<MeshFilter>();
        meshFilter.mesh = edgeMesh;

        meshRenderer = edgeVisual.AddComponent<MeshRenderer>();
        meshRenderer.material = edgeMaterial;

        for (int x = 0; x < resolutionX; x++)
        {
            for (int y = 0; y < resolutionY; y++)
            {
                List<List<Vector2i>> polygons = new List<List<Vector2i>>();

                List<Vector2i> vertices = new List<Vector2i>();
                vertices.Add(new Vector2i { x = x * blockSizeScaled, y = (y + 1) * blockSizeScaled });
                vertices.Add(new Vector2i { x = x * blockSizeScaled, y = y * blockSizeScaled });
                vertices.Add(new Vector2i { x = (x + 1) * blockSizeScaled, y = y * blockSizeScaled });
                vertices.Add(new Vector2i { x = (x + 1) * blockSizeScaled, y = (y + 1) * blockSizeScaled });

                polygons.Add(vertices);

                int idx = x + resolutionX * y;

                DestructibleBlock block = new DestructibleBlock();
                blocks[idx] = block;

                UpdateBlockBounds(x, y);

                block.UpdateSubEdgeMesh(polygons, depth);
            }
        }
               
        UpdateEdgeMesh();
    }

    private void UpdateBlockBounds(int x, int y)
    {
        int lx = x;
        int ly = y;
        int ux = x + 1;
        int uy = y + 1;

        if (lx == 0) lx = -1;
        if (ly == 0) ly = -1;
        if (ux == resolutionX) ux = resolutionX + 1;
        if (uy == resolutionY) uy = resolutionY + 1;

        BlockSimplification.currentLowerPoint = new Vector2i
        {
            x = lx * blockSizeScaled,
            y = ly * blockSizeScaled
        };

        BlockSimplification.currentUpperPoint = new Vector2i
        {
            x = ux * blockSizeScaled,
            y = uy * blockSizeScaled
        };
    }

    public void ExecuteClip(IClip clip, ClipType clipType = ClipType.Sub)
    {
        Vector2f worldPosition_f = transform.position;
        Vector2i worldPosition_i = worldPosition_f.ToVector2i();

        ClipBounds bounds = clip.GetBounds();
        int x1 = Mathf.Max(0, (int)((bounds.lowerPoint.x - worldPosition_f.x) / blockSize));
        if (x1 > resolutionX - 1) return;
        int y1 = Mathf.Max(0, (int)((bounds.lowerPoint.y - worldPosition_f.y) / blockSize));
        if (y1 > resolutionY - 1) return;
        int x2 = Mathf.Min(resolutionX - 1, (int)((bounds.upperPoint.x - worldPosition_f.x) / blockSize));
        if (x2 < 0) return;
        int y2 = Mathf.Min(resolutionY - 1, (int)((bounds.upperPoint.y - worldPosition_f.y) / blockSize));
        if (y2 < 0) return;

        Vector2i[] verts = clip.GetVertices();
 
        List<Vector2i> clipVertices = new List<Vector2i>(verts.Length);
        for (int i = 0; i < verts.Length; i++)
        {
            clipVertices.Add(new Vector2i(verts[i].x - worldPosition_i.x, verts[i].y - worldPosition_i.y));
        }     

        for (int x = x1; x <= x2; x++)
        {
            for (int y = y1; y <= y2; y++)
            {
                if (clip.CheckBlockOverlapping(new Vector2f((x + 0.5f) * blockSize + worldPosition_f.x, (y + 0.5f) * blockSize + worldPosition_f.y), blockSize))
                {
                    DestructibleBlock block = blocks[x + resolutionX * y];

                    List<List<Vector2i>> solutions = new List<List<Vector2i>>();

                    ClipperLib.Clipper clipper = new ClipperLib.Clipper();
                    clipper.AddPolygons(block.polygons, ClipperLib.PolyType.ptSubject);
                    clipper.AddPolygon(clipVertices, ClipperLib.PolyType.ptClip);
                    clipper.Execute(clipType == ClipType.Sub ? ClipperLib.ClipType.ctDifference : ClipperLib.ClipType.ctUnion, solutions,
                        ClipperLib.PolyFillType.pftNonZero, ClipperLib.PolyFillType.pftNonZero);

                    if (clipType == ClipType.Add)
                    {
                        List<Vector2i> squareClipper = new List<Vector2i>();
                        squareClipper.Add(new Vector2i(x * blockSizeScaled, (y + 1) * blockSizeScaled));
                        squareClipper.Add(new Vector2i(x * blockSizeScaled, y * blockSizeScaled));
                        squareClipper.Add(new Vector2i((x + 1) * blockSizeScaled, y * blockSizeScaled));
                        squareClipper.Add(new Vector2i((x + 1) * blockSizeScaled, (y + 1) * blockSizeScaled));

                        clipper = new ClipperLib.Clipper();
                        clipper.AddPolygons(solutions, ClipperLib.PolyType.ptSubject);
                        clipper.AddPolygon(squareClipper, ClipperLib.PolyType.ptClip);
                        clipper.Execute(ClipperLib.ClipType.ctIntersection, solutions,
                        ClipperLib.PolyFillType.pftNonZero, ClipperLib.PolyFillType.pftNonZero);
                    }

                    UpdateBlockBounds(x, y);

                    block.UpdateSubEdgeMesh(solutions, depth);
                }
            }
        }

        UpdateEdgeMesh();

        renderer.DrawBrushInTrunkSpace(clip.GetMesh(), worldPosition_f, new Vector2f(width, height), clipType);

        clipVertices.Clear();
    }

    public void UpdateEdgeMesh()
    {
        int totalVertexCount = 0;
        int totalTriangleCount = 0;

        for (int i = 0; i < blocks.Length; i++)
        {
            totalVertexCount += blocks[i].subVertices.Length;
            totalTriangleCount += blocks[i].subTriangles.Length;
        }

        Vector3[] vertices = new Vector3[totalVertexCount];
        Vector2[] texCoords = new Vector2[totalVertexCount];
        Vector3[] normals = new Vector3[totalVertexCount];
        int[] triangles = new int[totalTriangleCount];

        int vertexIndex = 0;
        int triangleIndex = 0;
        for (int i = 0; i < blocks.Length; i++)
        {
            int vertexCount = blocks[i].subVertices.Length;
            int triangleCount = blocks[i].subTriangles.Length;

            System.Array.Copy(blocks[i].subVertices, 0, vertices, vertexIndex, vertexCount);
            System.Array.Copy(blocks[i].subTexCoords, 0, texCoords, vertexIndex, vertexCount);
            System.Array.Copy(blocks[i].subNormals, 0, normals, vertexIndex, vertexCount);
            System.Array.Copy(blocks[i].subTriangles, 0, triangles, triangleIndex, triangleCount);

            for (int j = triangleIndex; j < triangleIndex + triangleCount; j++)
            {
                triangles[j] += vertexIndex;
            }

            vertexIndex += vertexCount;
            triangleIndex += triangleCount;
        }

        edgeMesh.Clear();
        edgeMesh.vertices = vertices;
        edgeMesh.uv = texCoords;
        edgeMesh.normals = normals;
        edgeMesh.triangles = triangles;
    }
}
