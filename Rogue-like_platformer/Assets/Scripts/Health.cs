using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
    public int maxHealth = 10;
    public Slider slider;

    public bool isPlayer;
    void Start()
    {
        health = maxHealth;
        if (slider != null)
        {  
        slider.maxValue = maxHealth;
        slider.value = health;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (slider != null)
        {slider.value = health;}

        if(health <=0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isPlayer)
        {
            Debug.Log("Player dead");
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
