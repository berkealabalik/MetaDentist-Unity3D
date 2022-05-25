using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField userNameInput;

    public static string username;
    public static string mail;


    public void SaveUsername(string newName)
    {
         username = newName;

    }

    public void playGame()
    {

        if (userNameInput != null)
        {
            username = userNameInput.text;
          
            Debug.Log("Play Game:" + username + " " + mail);
        }
   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("exiting");
    }
}
