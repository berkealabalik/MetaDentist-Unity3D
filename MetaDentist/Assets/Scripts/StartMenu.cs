using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
       
    }
  
   
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
