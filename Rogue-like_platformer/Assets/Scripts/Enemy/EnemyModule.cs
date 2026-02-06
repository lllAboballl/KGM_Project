using UnityEngine;

public abstract class EnemyModule : MonoBehaviour
{

protected Enemy enemy;

protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    public abstract void Tick();
}
