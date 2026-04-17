using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
    public int maxHealth = 10;
    public Slider slider;

    public bool isPlayer;

    ExperienceManager xpManager;
    EnemySpawner spawner;
    void Start()
    {
        xpManager = FindFirstObjectByType<ExperienceManager>();
        spawner = FindFirstObjectByType<EnemySpawner>();

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

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject);
            xpManager.totalXp += 3;
            xpManager.UpdateXpProgression();
            spawner.SpawnEnemy(Random.Range(0, 1));
            spawner.SpawnEnemy(0);
        }
    }
}
