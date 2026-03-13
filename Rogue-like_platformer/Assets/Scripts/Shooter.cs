using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletSize = 1f;

    Vector2 bulletDirection;

    Player_Controller playerController;
    InputAction attackAction;

    void Awake()
    {
       playerController = GetComponent<Player_Controller>();
       attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
       if (attackAction.WasReleasedThisFrame())
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Mathf.RoundToInt(playerController.GetMoveVector().x), Mathf.RoundToInt(playerController.GetMoveVector().y)) * bulletSpeed;
        bullet.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(Mathf.RoundToInt(playerController.GetMoveVector().y), Mathf.RoundToInt(playerController.GetMoveVector().x)) * Mathf.Rad2Deg;

        if (Mathf.RoundToInt(playerController.GetMoveVector().y) != 1)
        {
            bullet.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * playerController.GetPlayerDirection() * bulletSpeed;
            bullet.GetComponent<Rigidbody2D>().rotation = playerController.GetPlayerDirection();
        }
    }
}