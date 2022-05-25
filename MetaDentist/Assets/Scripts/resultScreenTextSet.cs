using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultScreenTextSet : MonoBehaviour
{
    public Text CanvasText;

 
    // Start is called before the first frame update
    void Start()
    {
        string combinedString = string.Join("", WriteResult.resultsArr.ToArray());
        CanvasText.text = combinedString;
    }
}
