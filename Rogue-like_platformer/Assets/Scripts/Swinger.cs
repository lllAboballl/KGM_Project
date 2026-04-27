using UnityEngine;

public class Swinger : MonoBehaviour
{
    [SerializeField] float swingSpeed = 0.5f;

    [SerializeField] Collider2D swingHitbox;
    [SerializeField] SpriteRenderer swingGraphics;

    [SerializeField] int swingBufferFrames;

    int swingBufferCounter = 0;
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

        SwingBuffer();

        if (!shooter.GetAiming()) { Swing(); }
       
        timeSinceLastSwing += Time.deltaTime;
    }

    void Swing()
    {
        if (timeSinceLastSwing < swingSpeed) { return; }

        if (swingBufferCounter > 0) 
        {
            animator.SetTrigger("swing");
            timeSinceLastSwing = 0;
        }
    }

    void SwingBuffer()
    {
        if (Input.GetButtonDown("Attack")) { swingBufferCounter =  swingBufferFrames; }

        else { swingBufferCounter--; }
    }
}