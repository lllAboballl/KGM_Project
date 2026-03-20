using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    //[SerializeField] float bulletSize = 1f;
    [SerializeField] float firerate = 0.5f;

    float timeSinceLastShot = 0;
    bool canShoot = false;

    bool aiming = false;

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

        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("shoot");
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
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * bulletSpeed;
        bullet.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")) * Mathf.Rad2Deg;

        if (Input.GetAxisRaw("Vertical") != 1)
        {
            bullet.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * playerController.GetPlayerDirection() * bulletSpeed;
            bullet.GetComponent<Rigidbody2D>().rotation = playerController.GetPlayerDirection();
        }
        
    }
}

