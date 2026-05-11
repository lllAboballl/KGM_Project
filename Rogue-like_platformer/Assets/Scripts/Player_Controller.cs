using UnityEngine;
using System;
using System.Collections;

public class Player_Controller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f; 
    [SerializeField] float jumpForce = 18f;

    [SerializeField] int jumpBufferFrames;
    [SerializeField] float coyoteTime;


    [Header("Ground Detection")]
    [SerializeField] float groundCheckDistanceX = 0.49f;
    [SerializeField] float groundCheckDistanceY = 0.49f; 
    [SerializeField] float groundCheckOffset = 0f;
    [SerializeField] LayerMask groundLayer;
    
    float playerDirection = 1;
    bool canMove = true;

    int jumpBufferCounter = 0;
    float coyoteTimeCounter = 0f;

    public bool isAlive = true;

    [HideInInspector] public PlayerStateList playerState;
    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer; 
    Animator animator;
   

    public static Player_Controller Instance;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }

        else { Instance = this; }
        DontDestroyOnLoad(gameObject);

        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerStateList>();
    }

    void Update()
    {
        if (playerState.transitioning == true) return;
        if (Time.timeScale == 0) { return; }

        Animations();

        if (!isAlive) { return; }

        playerDirection = Input.GetAxisRaw("Horizontal");
        TurnPlayer();

        if (!canMove) { return; }

        JumpBuffer();
        Jump();
    }

    void FixedUpdate()
    {
        if (!isAlive) { return; }

        if (!canMove) { playerRigidbody.linearVelocityX = 0; return; }

        MovePlayer();
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
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            playerRigidbody.linearVelocityY = jumpForce;
            animator.SetTrigger("jump");
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }
    }

    void JumpBuffer()
    {
        if (Input.GetButtonDown("Jump")) { jumpBufferCounter = jumpBufferFrames; }

        else { jumpBufferCounter--; }

        if (TouchingGround()) { coyoteTimeCounter = coyoteTime; }

        else { coyoteTimeCounter -= Time.deltaTime; }
    }

    public IEnumerator WalkIntoNewScene(Vector2 exitDirection, float delay)
    {
        if (exitDirection.y > 0)
        {
            playerRigidbody.linearVelocity = jumpForce * exitDirection;
        }
         
        if (exitDirection.x != 0)
        {
            playerDirection = exitDirection.x > 0 ? 1 : -1;
        }
        TurnPlayer();

        yield return new WaitForSeconds(delay);
        playerState.transitioning = false;

    }


    void TurnPlayer()
    {
        if (Time.timeScale == 0) { return; }

        if (playerDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (playerDirection > 0)
        {
            transform.localScale = Vector3.one;
        }
    }
    public float GetPlayerDirection()
    {
        return playerDirection;
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
        animator.SetBool("isFalling", playerRigidbody.linearVelocityY < 0);
    }
}

