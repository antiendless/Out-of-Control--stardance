using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class MainMenuController : MonoBehaviour
{
    private bool isCurrentlyLoading = false; //safety gate variable

    [Header("Menu Tracking Panel")]
    // Drag main floating Canvas buttons here to hide/show them during menu switches
    public GameObject mainButtonsController; //Main menu buttons
    public GameObject optionsMenuController; //Options menu buttons 
    public GameObject skinsMenuPanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void Start()
    {
        //safety check to ensure time physics are normal when returning from a level
        Time.timeScale = 1f;

        //ensure submenus are hidden are hidden by default when booting the game
        if (optionsMenuController != null) optionsMenuController.SetActive(false);
        if (skinsMenuPanel != null) skinsMenuPanel.SetActive(false);
        if (mainButtonsController != null) mainButtonsController.SetActive(true);
    }

    // play button logic
    public void PlayGame()
    {
        //if we're already running a load cycle, completely ignore any new triggers
        if (isCurrentlyLoading) return;

        isCurrentlyLoading = true; // lock the gate
        Debug.Log("Smoothly Launching Level 1...");
        Time.timeScale = 1f;

        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.SwitchSceneSmoothly("level 1");
        }
        else
        {
            SceneManager.LoadScene("level 1");
        }
    }

    //options button logic
    public void OpenOptions()
    {
        if (mainButtonsController != null) mainButtonsController.SetActive(false);
        if (optionsMenuController != null) optionsMenuController.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsMenuController != null) optionsMenuController.SetActive(false);
        if (mainButtonsController != null) mainButtonsController.SetActive(true);
    }

    //skins button logic
    public void OpenSkins()
    {
        if (mainButtonsController != null) mainButtonsController.SetActive(false);
        if (skinsMenuPanel != null) skinsMenuPanel.SetActive(true);
    }

    public void CloseSkins()
    {
        if (skinsMenuPanel != null) skinsMenuPanel.SetActive(false);
        if (mainButtonsController != null) mainButtonsController.SetActive(true);
    }

    //quit button logic
    public void QuitGame()
    {
        Debug.Log("The player clicked quit game. (Application closing...)");

        //this handles closing off the engine completely in your final compiled build execution loop
        Application.Quit();
    }
}
