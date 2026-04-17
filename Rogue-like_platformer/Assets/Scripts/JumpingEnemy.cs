using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] float checkDistance = 1f;
    [SerializeField] float wallCheckDistance = 0.1f;

    [SerializeField] bool isGrounded;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;

    [SerializeField] Transform lookPosition;
    [SerializeField] Transform groundCheckPosition;
    [SerializeField] Transform wallCheckPosition;

    Rigidbody2D enemyRigidbody;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(lookPosition.position, Vector2.down, checkDistance, groundLayer);
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, checkDistance / 2, groundLayer);
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheckPosition.position, Vector2.right, wallCheckDistance, groundLayer);
        RaycastHit2D enemyHit = Physics2D.Raycast(wallCheckPosition.position, Vector2.right, wallCheckDistance, enemyLayer);

        if (hit.collider == null && isGrounded)
        {
            transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        }

        if (groundHit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (wallHit.collider != null)
        {
            transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        }


        if (isGrounded)
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        enemyRigidbody.linearVelocityX = transform.right.x * moveSpeed;
    }

    void Jump()
    {
        enemyRigidbody.linearVelocityY = jumpForce;
    }


}