using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTerrainManager
{
    protected static DestructibleTerrainManager instance;

    protected List<IDestructible> subjectList = new List<IDestructible>();

    public static DestructibleTerrainManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DestructibleTerrainManager();
            }

            return instance;
        }
    }

    protected DestructibleTerrainManager()
    {

    }

    public void AddSubject(IDestructible subject)
    {
        subjectList.Add(subject);
    }

    public void Clip(IClip clipper, ClipType type)
    {
        for (int i = 0; i < subjectList.Count; i++)
        {
            subjectList[i].ExecuteClip(clipper, type);
        }
    }
}
