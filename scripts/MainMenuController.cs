using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Menu Tracking Panels")]
    public GameObject mainButtonsController; 
    public GameObject creditsMenuPanel; 

    private void Start()
    {
        Time.timeScale = 1f;

        // ensure the credits container is hidden by default when booting the game
        if (creditsMenuPanel != null) creditsMenuPanel.SetActive(false);
        if (mainButtonsController != null) mainButtonsController.SetActive(true);
    }

    public void PlayGame()
    {
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

    // credits panel toggles
    public void OpenCredits()
    {
        if (mainButtonsController != null) mainButtonsController.SetActive(false);
        if (creditsMenuPanel != null) creditsMenuPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        if (creditsMenuPanel != null) creditsMenuPanel.SetActive(false);
        if (mainButtonsController != null) mainButtonsController.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Application closing...");
        Application.Quit();
    }
}
