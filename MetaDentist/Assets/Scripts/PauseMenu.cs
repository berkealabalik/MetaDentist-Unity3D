using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private float timer = 0;
    private float duration = 0.10f;
    // Update is called once per frame
    void Update()
    {

        if (Gamepad.all[0].startButton.isPressed|| Input.GetKeyDown(KeyCode.Escape))
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                timer = 0;
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }



    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.Locked;

    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadResultsScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ResultsScene");
    }
    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }
}
