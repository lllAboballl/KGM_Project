using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f; 
    [SerializeField] float jumpForce = 18f;

    [Header("Ground Detection")]
    [SerializeField] float groundCheckDistanceX = 0.49f;
    [SerializeField] float groundCheckDistanceY = 0.49f; 
    [SerializeField] float groundCheckOffset = 0f;
    [SerializeField] LayerMask groundLayer;
    
    int playerDirection = 1;
    bool canMove = true;

    public bool isAlive = true;
    

    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer; 
    Animator animator;
    
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0) { return; }

        Animations();

        if (!isAlive) { return; }

        TurnPlayer();

        if (!canMove) { return; }

        Jump();
    }

    void FixedUpdate()
    {
        if (!isAlive) { return; }

        if (!canMove) { playerRigidbody.linearVelocityX = 0; return; }

        MovePlayer();
    }

    public int GetPlayerDirection()
    {
        return playerDirection;
    }

    void MovePlayer()
    {
        playerRigidbody.linearVelocityX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    bool TouchingGround()
    {
        Collider2D isGrounded = Physics2D.OverlapBox(transform.position - new Vector3(groundCheckOffset, 0.4f, 0),
            new Vector2(groundCheckDistanceX, groundCheckDistanceY), 0, groundLayer);
        return isGrounded;
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump") && TouchingGround())
        {
            playerRigidbody.linearVelocityY = jumpForce;
        }
    }
    void TurnPlayer()
        {
        if (Time.timeScale == 0) { return; }

        if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                //spriteRenderer.flipX = true;
                playerDirection = -1;
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.localScale = Vector3.one;
                //spriteRenderer.flipX = false;
                playerDirection = 1;
            }
        }

    void EnableMovement()
    {
        canMove = true;
    }

    void DisableMovement()
    {
        if (TouchingGround())
        canMove = false;
    }

    void Animations()
    {
        animator.SetBool("isMoving", playerRigidbody.linearVelocityX != 0 && Input.GetAxisRaw("Horizontal") != 0);
        animator.SetBool("isGrounded", TouchingGround());
    }
}

