using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WriteResult : MonoBehaviour
{

    string myFilePath, fileName;
    string[] myNum = { "10", "20"};
    void Start()
    {
        fileName = "result.txt";
        myFilePath = Application.dataPath + "/" + fileName;
    }

    void Update()
    {



    }

    public void createNewResultFile()
    {
        
        File.WriteAllLines(myFilePath, myNum);
    }

}