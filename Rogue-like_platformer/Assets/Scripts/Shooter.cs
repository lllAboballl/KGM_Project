using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    //[SerializeField] float bulletSize = 1f;
    [SerializeField] float firerate = 0.5f;

    float timeSinceLastShot = 0;
    bool canShoot = false;

    Player_Controller playerController;
    Animator animator;
    GameObject bullet;

    void Awake()
    {
        playerController = GetComponent<Player_Controller>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Shoot();
        timeSinceLastShot += Time.deltaTime;
    }

    void Shoot()
    {


        if (Input.GetButton("Aim"))
        {
            animator.SetBool("aiming", true);
            playerController.canMove = false;
        }
        else
        {
            animator.SetBool("aiming", false);
            playerController.canMove = true;
            canShoot = false;
        }

        if (!canShoot || timeSinceLastShot < firerate) { return; }

        if (Input.GetButtonDown("Attack"))
        {
            SpawnBullet();
            timeSinceLastShot = 0;
        }
    }

    void EnableShooting()
    {
        canShoot = true;
    }

    public bool GetCanShoot()
    {
        return canShoot;
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

