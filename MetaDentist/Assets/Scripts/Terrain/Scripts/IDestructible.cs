using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible 
{
    void ExecuteClip(IClip clip, ClipType clipType = ClipType.Sub);
}
