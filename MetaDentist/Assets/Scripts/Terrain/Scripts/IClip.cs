using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2i = ClipperLib.IntPoint;

public enum ClipType
{ 
    Add,
    Sub
}

public struct ClipBounds
{
    public Vector2 lowerPoint;
    public Vector2 upperPoint;

    public bool Overlap(ClipBounds other)
    {
        return true;
    }
}

public interface IClip
{
    ClipBounds GetBounds();

    Vector2i[] GetVertices();

    Mesh GetMesh();

    bool CheckBlockOverlapping(Vector2 p, float size);
}
