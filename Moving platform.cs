using UnityEngine;

public class Movingplatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector2 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == (Vector2)pointB.position) ? pointA.position : pointB.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}
