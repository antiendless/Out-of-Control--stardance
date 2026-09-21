using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
  
  public string nextLevelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if object is touching the player 
        if (collision.CompareTag("Player"))
        {
            //load next level scene
            SceneManager.LoadScene(nextLevelName);
        }
    } 
}
