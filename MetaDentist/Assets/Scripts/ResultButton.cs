using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void loadMainScene()
    {
        SceneManager.LoadScene(0);
    }
    public void loadGameScene()
    {
        SceneManager.LoadScene(1);
    }


}
