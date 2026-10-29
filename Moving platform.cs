using UnityEngine;

public class Movingplatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private Vector2 platformVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = pointB.position;
    } 

    void FixedUpdate()
    {
        //calculate next target point using physics intervals 
        Vector2 currentPos = rb.position;
        Vector2 nextPos = Vector2.MoveTowards(currentPos, targetPosition, speed * Time.fixedDeltaTime);
        
        //calculate the platforms velocity 
        platformVelocity = (nextPos - currentPos) / Time.fixedDeltaTime;

        //hi idk this line is just moving the kinematic rigidbody
        rb.MovePosition(nextPos);

        //check if the platform has reached the target point
        if (Vector2.Distance(nextPos, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == (Vector2)pointB.position) ? pointA.position : pointB.position;
        }
    }

    //grip attachment
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                //smoothly inject the platform's movement velocity directly into the player
                playerRb.position += platformVelocity * Time.fixedDeltaTime;
            }
        }
    }
}