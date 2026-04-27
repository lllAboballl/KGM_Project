using UnityEngine;

public class Bullet : MonoBehaviour
{
   

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) { return; }
        else Destroy(gameObject);
    }
}
