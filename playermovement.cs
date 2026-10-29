using UnityEngine;

public class playermovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float horizontal;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    
    // Flags for jumping
    private bool jumpRequested = false;
    private bool jumpHolding = false;

    [Header("Hold Jump")]
    public float maxJumpTime = 0.3f;
    public float holdJumpForce = 15f;
    private bool isJumping;
    private float jumpTimeCounter;

    [Header("Respawn settings")]
    public float fallThreshold = -10f;
    private Vector2 spawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        spawnPosition = transform.position;
    }

    void Update()
    {
        // 1. Handle Random Controls if ControlManager exists
        if (ControlManager.Instance != null)
        {
            // Reset horizontal each frame so the player stops when keys are released
            horizontal = 0f;

            if (Input.GetKey(ControlManager.Instance.moveLeftKey))
            {
                horizontal = -1f;
            }
            else if (Input.GetKey(ControlManager.Instance.moveRightKey))
            {
                horizontal = 1f;
            }
        
            // Jumping initial press
            if (Input.GetKeyDown(ControlManager.Instance.jumpKey) && IsGrounded())
            {
                jumpRequested = true;
                isJumping = true;
                jumpTimeCounter = maxJumpTime;
            }

            // Continue jumping 
            if (Input.GetKey(ControlManager.Instance.jumpKey) && isJumping)
            {
                jumpHolding = true;
            }
            else
            {
                jumpHolding = false;
            }

            // Stop jumping
            if (Input.GetKeyUp(ControlManager.Instance.jumpKey))
            {
                isJumping = false;
                jumpHolding = false;
            }
        }
        // 2. Fallback Controls (Runs ONLY if ControlManager is completely missing from the scene)
        else
        {
            horizontal = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                jumpRequested = true;
                isJumping = true;
                jumpTimeCounter = maxJumpTime;
            }

            if (Input.GetButton("Jump") && isJumping)
            {
                jumpHolding = true;
            }
            else
            {
                jumpHolding = false;
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
                jumpHolding = false;
            }
        }

        // Check if player has fallen off the map
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = spawnPosition;
        rb.linearVelocity = Vector2.zero; 
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }

        if (jumpHolding && jumpTimeCounter > 0)
        {
            rb.AddForce(new Vector2(0, holdJumpForce), ForceMode2D.Force);
            jumpTimeCounter -= Time.fixedDeltaTime;
        }
        else
        {
            isJumping = false;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
// hello! 