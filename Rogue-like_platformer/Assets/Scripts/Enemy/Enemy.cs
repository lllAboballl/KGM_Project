using UnityEngine;

public class Enemy : MonoBehaviour
{

[HideInInspector]
public Transform target;
public float moveSpeed = 3f;
private EnemyModule[] modules;
[HideInInspector]
public Vector3 lastKnownTargetPosition;

void Awake()
    {
        modules = GetComponents<EnemyModule>();
    }

void Update()
    {
        foreach (var module in modules)
        {
            module.Tick();
        }
    }
public void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

}