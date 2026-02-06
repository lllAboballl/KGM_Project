using UnityEngine;
using System.Collections.Generic;
public class Damage : MonoBehaviour
{

    [Header("Damage")]
    public int damage = 2;
    [Header("Stun")]
    public float stunTime = 0.5f;
    [Header("Player hit down")]
    public float playerHitCooldown = 1f;

    private Dictionary<Health, float> playerLastHitTime = new Dictionary<Health, float>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamage(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DealDamage(other.gameObject);
    }
    void DealDamage(GameObject target)
    {
        Health health = target.GetComponent<Health>();
        if (health == null) return;
        bool isPlayer = health.isPlayer;
        if (isPlayer)
        {
            if (playerLastHitTime.TryGetValue(health, out float lastTime))
            {
                if (Time.time < lastTime + playerHitCooldown)
                return;
            }

            health.TakeDamage(damage);
            playerLastHitTime[health] = Time.time;
            return;
        }

        health.TakeDamage(damage);
        ApplyStun(target,stunTime);
    }
        

        void ApplyStun(GameObject enemy, float duration)
        {
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            MonoBehaviour ai = enemy.GetComponent<MonoBehaviour>();

            if (rb != null)
            rb.linearVelocity = Vector2.zero;

            if (ai != null)
            ai.enabled = false;

            StartCoroutine(StunCoroutine(ai, duration));
        }

        System.Collections.IEnumerator StunCoroutine(MonoBehaviour ai, float duration)
        {
            yield return new WaitForSeconds(duration);

            if (ai!= null)
            ai.enabled = true;
        }
    }


    


