/*using UnityEngine;

public class EnemyTargetModule : EnemyModule
{
   
public float viewDistance = 5f;
public string targetTag = "Player";

    public override void Tick()
    {
     FindTarget();   
    }

private void FindTarget()
    {
        GameObject player = GameObject.FindWithTag(targetTag);
        if (player == null)
        {
            enemy.target = null;
            return;
        }
        float distance = Vector3.Distance(enemy.transform.position,player.transform.position);
        if (distance <= viewDistance)
        {
            enemy.target = player.transform;
            enemy.lastKnownTargetPosition = player.transform.position;
        }
        else
        {
            enemy.target = null;
        }
    }
}*/
