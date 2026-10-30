using UnityEngine;

public class edenhazardscriptykwim : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if player touching the spike is actually the player or not 
        if (collision.CompareTag("Player") &&
            collision.TryGetComponent<playermovement>(
                out playermovement player))
        {
            player.Respawn();
            //empty space hahahahaa anyways back to the code
                //haha another empty line where i can add another code comment, dont mind if i dooo
                //another empty line for a code comment??? hell yeah
                Debug.Log("Player has hit a hazard and respawned");
            
        }
    }
}