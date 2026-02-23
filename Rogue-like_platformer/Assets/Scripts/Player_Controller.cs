using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player_Controller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 16f;

    [Header("Ground Detection")]
    [SerializeField] float groundCheckDistanceX = 0.49f;
    [SerializeField] float groundCheckDistanceY = 0.49f;
    [SerializeField] float groundCheckOffset = 0.05f;
    [SerializeField] LayerMask groundLayer;

    [Header("Ledge Detection")]
    [SerializeField] float ledgeCheckDistance = 0.5f;
    [SerializeField] float airLedgeCheckDistance = 0.8f;
    [SerializeField] Vector3 ledgeGrabTarget;
    [SerializeField] float ledgeGrabSpeed;
    int playerDirection;

    Vector3 playerPosition;
    Vector2 moveVector;
    bool isAlive = true;
    bool canMove = true;
   
    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    InputAction moveAction;

    InputAction jumpAction;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        Debug.Log(canMove);
        moveVector = moveAction.ReadValue<Vector2>();

        if (isAlive)
        {
            TurnPlayer();
            LedgeGrabCheck();
        }

    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            MovePlayer();
            Jump();
        }
     }
     public Vector2 GetMoveVector()
{
    return moveVector;
}

public int GetPlayerDirection()
{
    return playerDirection;
}

         void MovePlayer()
    {
        if (canMove)
        {
            playerRigidbody.linearVelocityX = moveVector.x * moveSpeed;
        }
        
    }

    bool TouchingGround()
    {
        //Checks if the player is grounded with an invisible box on the player. If the box touches a groundlayer object it returns the name of the object, otherwise it's null.
        Collider2D isGrounded = Physics2D.OverlapBox(transform.position - new Vector3(groundCheckOffset, 0.4f, 0),
            new Vector2(groundCheckDistanceX, groundCheckDistanceY), 0, groundLayer);

        return isGrounded;
    }

    void Jump()
    {
        if (jumpAction.IsPressed() && TouchingGround() && canMove)
        {
            playerRigidbody.linearVelocityY = jumpForce;
        }
    }
    void TurnPlayer()
 {
     if (moveVector.x < 0)
     {
         spriteRenderer.flipX = true;
         playerDirection = -1;
     }
     if (moveVector.x > 0)
     {
         spriteRenderer.flipX = false;
         playerDirection = 1;
     }
 }
    void LedgeGrabCheck()
{
    Vector3 targetOriented = ledgeGrabTarget;
    targetOriented.x *= moveVector.x;
    RaycastHit2D hit1 = Physics2D.Raycast(transform.position + new Vector3(0, 0.3f, 0), new Vector2(moveVector.x, 0), airLedgeCheckDistance, groundLayer);
    if (hit1.collider != null) { return; }

    RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(moveVector.x, 0), ledgeCheckDistance, groundLayer);

    if (hit2.collider == null) { return; }

    if (!jumpAction.IsPressed()) { return; }

    StartCoroutine(LedgeGrabRoutine(targetOriented));

    IEnumerator LedgeGrabRoutine(Vector3 targetPosition)
    {
        //Play animation here

        playerRigidbody.linearVelocity = Vector2.zero;
        playerRigidbody.gravityScale = 0;
        canMove = false;

        Vector3 targetPositionWorld = transform.position + targetPosition;

        while (transform.position.y < targetPositionWorld.y)
        {
            transform.Translate(Vector2.up * Time.deltaTime * ledgeGrabSpeed);

            yield return null;
        }

        while (targetPosition.x < 0 && transform.position.x > targetPositionWorld.x || targetPosition.x > 0 && transform.position.x < targetPositionWorld.x)
        {
            if (targetPosition.x < 0)
            {
                transform.Translate(Vector2.left * Time.deltaTime * ledgeGrabSpeed);
            }

            if (targetPosition.x > 0)
            {
                transform.Translate(Vector2.right * Time.deltaTime * ledgeGrabSpeed);
            }
            yield return null;
        }

        playerRigidbody.linearVelocity = Vector2.zero;
        playerRigidbody.gravityScale = 5;
        canMove = true;
    }
}
}

