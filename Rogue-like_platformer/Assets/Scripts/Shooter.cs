using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletSize = 1f;
    [SerializeField] float firerate = 2f;

    float timeSinceLastShot = 0;
    bool canShoot = false;

    Player_Controller playerController;
    Animator animator;

    void Awake()
    {
        playerController = GetComponent<Player_Controller>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        Shoot();
        if (timeSinceLastShot >= firerate)


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
        }

        if (!canShoot) { return; }

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

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);



        if (Input.GetAxisRaw("Vertical") != 1)
        {
            bullet.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * playerController.GetPlayerDirection() * bulletSpeed;
            bullet.GetComponent<Rigidbody2D>().rotation = playerController.GetPlayerDirection();
        }
    }
}

