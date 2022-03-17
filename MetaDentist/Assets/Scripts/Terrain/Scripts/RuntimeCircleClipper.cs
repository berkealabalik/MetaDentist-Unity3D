using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2i = ClipperLib.IntPoint;
using Vector2f = UnityEngine.Vector2;
using ClipperLib;

public class RuntimeCircleClipper : MonoBehaviour, IClip
{
    public ClipType clipType = ClipType.Sub;
    private struct TouchLineOverlapCheck 
    {
        public float a;
        public float b;
        public float c;
        public float angle;
        public float dividend;

        public TouchLineOverlapCheck(Vector2 p1, Vector2 p2)
        {
            Vector2 d = p2 - p1;
            float m = d.magnitude;
            a = -d.y / m;
            b = d.x / m;
            c = -(a * p1.x + b * p1.y);
            angle = Mathf.Rad2Deg * Mathf.Atan2(-a, b);

            float da;
            if (d.x / d.y < 0f)
                da = 45 + angle;
            else
                da = 45 - angle;

            dividend = Mathf.Abs(1.0f / 1.4f * Mathf.Cos(Mathf.Deg2Rad * da));
        }

        public float GetDistance(Vector2 p)
        {
            return Mathf.Abs(a * p.x + b * p.y + c);
        }
    }

    public float diameter = 1.2f;

    public float radius { get { return diameter / 2f; } }

    public int segmentCount = 10;

    public float touchMoveDistance = 0.1f;

    private Vector2f currentTouchPoint;

    private Vector2f previousTouchPoint;

    private TouchPhase touchPhase;

    private TouchLineOverlapCheck touchLine;

    private Vector2i[] vertices;

    private Camera mainCamera;

    private float cameraZPos;

    private Mesh mesh;

    public bool CheckBlockOverlapping(Vector2f p, float size)
    {
        if (touchPhase == TouchPhase.Began)
        {
            float dx = Mathf.Abs(currentTouchPoint.x - p.x) - radius - size / 2;
            float dy = Mathf.Abs(currentTouchPoint.y - p.y) - radius - size / 2;
            return dx < 0f && dy < 0f;
        }
        else if (touchPhase == TouchPhase.Moved)
        {          
            float distance = touchLine.GetDistance(p) - radius - size / touchLine.dividend;
            return distance < 0f;
        }
        else
            return false;
    }

    public ClipBounds GetBounds()
    {
        if (touchPhase == TouchPhase.Began)
        {
            return new ClipBounds
            {
                lowerPoint = new Vector2f(currentTouchPoint.x - radius, currentTouchPoint.y - radius),
                upperPoint = new Vector2f(currentTouchPoint.x + radius, currentTouchPoint.y + radius)
            };
        }
        else if (touchPhase == TouchPhase.Moved)
        {
            Vector2f upperPoint = currentTouchPoint;
            Vector2f lowerPoint = previousTouchPoint;
            if (previousTouchPoint.x > currentTouchPoint.x)
            {
                upperPoint.x = previousTouchPoint.x;
                lowerPoint.x = currentTouchPoint.x;
            }
            if (previousTouchPoint.y > currentTouchPoint.y)
            {
                upperPoint.y = previousTouchPoint.y;
                lowerPoint.y = currentTouchPoint.y;
            }

            return new ClipBounds
            {
                lowerPoint = new Vector2f(lowerPoint.x - radius, lowerPoint.y - radius),
                upperPoint = new Vector2f(upperPoint.x + radius, upperPoint.y + radius)
            };
        }
        else
            return new ClipBounds();
    }

    public Vector2i[] GetVertices()
    {
        return vertices;
    }

    public Mesh GetMesh()
    {
        return mesh;
    }

    void Awake()
    {
        mainCamera = Camera.main;
        cameraZPos = mainCamera.transform.position.z;

        mesh = new Mesh();
        mesh.MarkDynamic();
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateTouch();
    }

    void UpdateTouch()
    {
        if (TouchUtility.Enabled && TouchUtility.TouchCount > 0)
        {
            Touch touch = TouchUtility.GetTouch(0);
            Vector2 touchPosition = touch.position;

            touchPhase = touch.phase;
            if (touch.phase == TouchPhase.Began)
            {
                currentTouchPoint = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, -cameraZPos));

                Build(currentTouchPoint);

                DestructibleTerrainManager.Instance.Clip(this, clipType);

                previousTouchPoint = currentTouchPoint;            
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPoint = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, -cameraZPos));
 
                if ((currentTouchPoint - previousTouchPoint).sqrMagnitude <= touchMoveDistance * touchMoveDistance)
                    return;
               
                Build(previousTouchPoint, currentTouchPoint);

                DestructibleTerrainManager.Instance.Clip(this, clipType);

                previousTouchPoint = currentTouchPoint;
            }
        }
    }

    void Build(Vector2 center)
    {
        Vector3[] meshVertices = new Vector3[segmentCount];
        Vector3[] meshNormals = new Vector3[segmentCount];
        vertices = new Vector2i[segmentCount];

        for (int i = 0; i < segmentCount; i++)
        {
            float angle = Mathf.Deg2Rad * (-90f - 360f / segmentCount * i);

            Vector2 point = new Vector2(center.x + radius * Mathf.Cos(angle), center.y + radius * Mathf.Sin(angle));
            vertices[i] = point.ToVector2i();

            meshVertices[i] = point.ToVector3f();
            meshNormals[i] = (meshVertices[i] - center.ToVector3f()) / radius;
        }

        mesh.Clear();
        mesh.vertices = meshVertices;
        mesh.normals = meshNormals;
        mesh.triangles = Triangulate.Execute(meshVertices).ToArray();
    }

    void Build(Vector2 begin, Vector2 end)
    {
        int halfSegmentCount = segmentCount / 2;
        touchLine = new TouchLineOverlapCheck(begin, end);

        Vector3[] meshVertices = new Vector3[segmentCount + 2];
        Vector3[] meshNormals = new Vector3[segmentCount + 2];
        vertices = new Vector2i[segmentCount + 2];
              
        for (int i = 0; i <= halfSegmentCount; i++)
        {
            float angle = Mathf.Deg2Rad * (touchLine.angle + 270f - 360f / segmentCount * i);

            Vector2 point = new Vector2(begin.x + radius * Mathf.Cos(angle), begin.y + radius * Mathf.Sin(angle));
            vertices[i] = point.ToVector2i();

            meshVertices[i] = point.ToVector3f();
            meshNormals[i] = (meshVertices[i] - begin.ToVector3f()) / radius;
        }

        for (int i = halfSegmentCount; i <= segmentCount; i++)
        {
            float angle = Mathf.Deg2Rad * (touchLine.angle + 270f - 360f / segmentCount * i);

            Vector2 point = new Vector2(end.x + radius * Mathf.Cos(angle), end.y + radius * Mathf.Sin(angle));
            vertices[i+1] = point.ToVector2i();

            meshVertices[i+1] = point.ToVector3f();
            meshNormals[i+1] = (meshVertices[i+1] - end.ToVector3f()) / radius;
        }      

        mesh.Clear();
        mesh.vertices = meshVertices;
        mesh.normals = meshNormals;
        mesh.triangles = Triangulate.Execute(meshVertices).ToArray();
    }
}
