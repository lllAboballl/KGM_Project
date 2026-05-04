using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletSize = 1f;
    [SerializeField] float firerate = 0.5f;

    [SerializeField] int shootBufferFrames;

    float timeSinceLastShot = 0;
    bool canShoot = false;
    bool aiming = false;

    int shootBufferCounter = 0;

    Player_Controller playerController;
    Swinger swinger;
    Animator animator;
    GameObject bullet;

    void Awake()
    {
        playerController = GetComponent<Player_Controller>();
        swinger = GetComponent<Swinger>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0) { return; }

        ShootBuffer();
        Aim();
        timeSinceLastShot += Time.deltaTime;
    }

    void Aim()
    {
        if (Input.GetButton("Aim"))
        {
            animator.SetBool("aiming", true);
            aiming = true;
        }
        else
        {
            animator.SetBool("aiming", false);
            animator.SetBool("canShoot", false);
            canShoot = false;
            aiming = false;
        }

        if (!canShoot || timeSinceLastShot < firerate) { return; }

        if (shootBufferCounter > 0)
        {
            animator.SetTrigger("shoot");
            shootBufferCounter = 0;
        }
    }

    void Shoot()
    {
        if (canShoot) 
        {
            SpawnBullet();
            timeSinceLastShot = 0;
        }
    }

    void ShootBuffer()
    {
        if (Input.GetButtonDown("Attack")) 
        { shootBufferCounter = shootBufferFrames; }

        else { shootBufferCounter--; }
    }

    void EnableShooting()
    {
        canShoot = true;
        animator.SetBool("canShoot", true);
    }

    public bool GetCanShoot()
    {
        return canShoot;
    }

    public bool GetAiming()
    {
        return aiming;
    }

    void SpawnBullet()
    {
        bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0.2f * playerController.GetPlayerDirection(), 0.25f, 0), Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * bulletSpeed;
        bullet.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")) * Mathf.Rad2Deg;

        if (Input.GetAxisRaw("Vertical") != 1)
        {
            bullet.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * playerController.GetPlayerDirection() * bulletSpeed;
            bullet.GetComponent<Rigidbody2D>().rotation = playerController.GetPlayerDirection();
        }
    }
}

