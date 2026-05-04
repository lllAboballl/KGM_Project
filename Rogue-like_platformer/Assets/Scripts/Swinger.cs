using UnityEngine;

public class Swinger : MonoBehaviour
{
    [SerializeField] float swingSpeed = 0.5f;

    [Header("Hi")]
    [SerializeField] Collider2D swingHitbox;
    [SerializeField] SpriteRenderer swingGraphics;
    
    float timeSinceLastSwing = 0;
    
    Player_Controller playerController;
    Shooter shooter;
    Animator animator;

    void Awake()
    {
        playerController = GetComponent<Player_Controller>();
        shooter = GetComponent<Shooter>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0) { return; }

        if (!shooter.GetAiming()) { Swing(); }
       
        timeSinceLastSwing += Time.deltaTime;
    }

    void Swing()
    {
        if (timeSinceLastSwing < swingSpeed) { return; }

      
        
        if (Input.GetButtonDown("Attack")) 
        {
            animator.SetTrigger("swing");
            timeSinceLastSwing = 0;
        }
        
    }
}