using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [Header("Transition Settings")]
    public CanvasGroup fadeCanvasGroup; //*note to self* remember to drag scene folder here
    public float fadeDuration = 0.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // this keeps fader live across level transitions
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwitchSceneSmoothly(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
       // force unpause time scales so the engine processes frames
        Time.timeScale = 1f;

        //lock screen layer so player can't click things twice
       if (fadeCanvasGroup != null) fadeCanvasGroup.blocksRaycasts = true;

       // fade out to black screen overlay *before* loading the screen
       float timer = 0f;
       while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            if (fadeCanvasGroup != null) fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        // ensure that its fully black
        if (fadeCanvasGroup != null) fadeCanvasGroup.alpha = 1f;
        

        //then: load target scene file asset layout unsynchronised
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // wait for a lil while for level 1 to settle physics link
        yield return new WaitForSecondsRealtime(0.1f);
        

        //fade back smoothly to gameplay environment
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            if (fadeCanvasGroup != null) fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }

        //make it fully transparent and unlock raycasts again
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.blocksRaycasts = false;
        }
    }
    
}
