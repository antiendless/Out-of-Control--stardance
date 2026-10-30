using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance;

    //Current Active Keys
    [HideInInspector] public KeyCode moveLeftKey = KeyCode.A;
    [HideInInspector] public KeyCode moveRightKey = KeyCode.D;
    [HideInInspector] public KeyCode jumpKey = KeyCode.Space;

    //List of possible keys to choose from
    private List<KeyCode> poolOfKeys = new List<KeyCode>()
    {
        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D,
        KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.Space, KeyCode.LeftShift, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.E, KeyCode.Q
    };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //regisers to listen for level change
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "level 1" || scene.name == "MainSetup")
        {
            ResetToDefaultControls();
        }
        else
        {
            RandomizeControls();
        }
    }
    
    public void ResetToDefaultControls()
    {
        moveLeftKey = KeyCode.A;
        moveRightKey = KeyCode.D;
        jumpKey = KeyCode.Space;
        Debug.Log("Controls reset to default (A, D, Space)");
    }

    public void RandomizeControls()
    {
        //shuffling keys to get random mappings
        List<KeyCode> availableKeys = new(poolOfKeys);

        //random key for left 
        int randLeft = Random.Range(0, availableKeys.Count);
        moveLeftKey  = availableKeys[randLeft];
        availableKeys.RemoveAt(randLeft); 

        //random key for right
        int randRight = Random.Range(0, availableKeys.Count);
        moveRightKey = availableKeys[randRight];
        availableKeys.RemoveAt(randRight);

        //random key for jump
        int randJump = Random.Range(0, availableKeys.Count);
        jumpKey = availableKeys[randJump];

        Debug.Log($"controls scrambled!! Left: {moveLeftKey} | Right: {moveRightKey} | Jump: {jumpKey}");
    

    }
 
}
