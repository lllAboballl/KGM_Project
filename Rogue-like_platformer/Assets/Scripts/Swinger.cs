using UnityEngine;

public class Swinger : MonoBehaviour
{
    [SerializeField] float swingSpeed = 1f;
    [Header("Hi")]
    [SerializeField] Collider2D swingHitbox;
    [SerializeField] SpriteRenderer swingGraphics;
    [SerializeField] Animator animator;

    float swingCooldown = 1f;

    void Awake()
    {
        DisableSwinger();
    }

    void Update()
    {
        Swing();

        Debug.Log(swingCooldown);
    }

    void Swing()
    {
        if (swingCooldown > 0) { swingCooldown -= Time.deltaTime; return; }
        swingCooldown = swingSpeed;
        if (!Input.GetButtonDown("Fire1")) { return; }
        //animator.Play()

    }

    //--------Animator stuff---------\\
    void EnableSwinger()
    { 
        swingHitbox.enabled = true;
        swingGraphics.enabled = true;
    }

    void DisableSwinger()
    {
        swingHitbox.enabled = false; 
        swingGraphics.enabled = false; 
    }



}