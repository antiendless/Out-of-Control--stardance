using UnityEngine;
using UnityEngine.SceneManagement; //this is needed to go back to the main menu btw

public class PauseMenuScript : MonoBehaviour
{
    public static bool IsGamePaused = false;

    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject optionsMenuPanel;

        // Update is called once per frame
    void Update()
    {
        //waits for escape key to be pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                //if options menu is open, make Escape close it first instead of instantly unpausing 
                if (optionsMenuPanel.activeSelf)
                {
                    CloseOptions();
                }
                else
                {
                    ResumeGame();
                }

            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);

        Time.timeScale = 1f; //unfreezes the physics engine time loop
        IsGamePaused = false;
    }

    void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        optionsMenuPanel.SetActive(false);

        Time.timeScale = 0f; //freezes all physics movement and delta time calculations completely
        IsGamePaused = true;
    }

    public void OpenOptions()
    {
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; //reset time flow before switching scenes, or the next scene will be frozen twin
        SceneManager.LoadScene("MainMenu"); //this is the scene name, not the build index. Make sure to spell it correctly or it won't work! 
    }

}
