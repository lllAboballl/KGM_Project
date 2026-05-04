using UnityEngine;
using UnityEngine.SceneManagement;
public class ChargeEnemy : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float chargeSpeed = 10f;
    [SerializeField] float checkDistance = 1f;
    [SerializeField] float chargeCheckDistance = 10f;
    [SerializeField] float wallCheckDistance = 0.1f;

    [SerializeField] bool isCharging = false;
    [SerializeField] float chargeTime = 3f;

    [SerializeField] bool isGrounded;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] Transform lookPosition;
    [SerializeField] Transform groundCheckPosition;
    [SerializeField] Transform forwardCheckPosition;

    float currentSpeed;

    Rigidbody2D enemyRigidbody;

    EnemySoundManager enemySoundManager;

    void Awake()
    {
        enemySoundManager = GetComponent<EnemySoundManager>();

        enemyRigidbody = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(lookPosition.position, Vector2.down, checkDistance, groundLayer);
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, checkDistance / 2, groundLayer);
        RaycastHit2D wallHit = Physics2D.Raycast(forwardCheckPosition.position, Vector2.right, wallCheckDistance, groundLayer);
        RaycastHit2D chargeHit = Physics2D.Raycast(forwardCheckPosition.position, Vector2.right, chargeCheckDistance, playerLayer);

        if (hit.collider == null && isGrounded)
        {
            transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        }

        if (groundHit.collider != null)
        {
            isGrounded = true;
            enemySoundManager.PlayLandingSFX();
        }
        else
        {
            isGrounded = false;
        }

        if (wallHit.collider != null)
        {
            transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        }

        if (chargeHit.collider != null)
        {
            if (!isCharging)
            {
                isCharging = true;
                StartCharge();
            }

        }
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        enemyRigidbody.linearVelocityX = transform.right.x * currentSpeed;
    }

    void StartCharge()
    {
        enemySoundManager.PlayChargingSFX();
        currentSpeed = 0f;
        Invoke(nameof(Charge), chargeTime);

    }

    void Charge()
    {
        currentSpeed = chargeSpeed;
        Invoke(nameof(EndCharge), chargeTime);
    }

    void EndCharge()
    {
        currentSpeed = moveSpeed;
        isCharging = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (other.gameObject.layer == layerIndex && isCharging)
        {

        }

    }
}