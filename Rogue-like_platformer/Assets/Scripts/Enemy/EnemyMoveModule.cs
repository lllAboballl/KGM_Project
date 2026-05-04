using UnityEngine;

public class EnemyMoveModule : EnemyModule
{
   
[Header("Patrol Points")]
public Transform patrolPointA;
public Transform patrolPointB;
[Header("Movement Settings")]
public float patrolSpeed = 2f;
public float searchRadius = 3f;
public float searchTime = 5f;

private float searchTimer;
private Vector3 currentTarget;
private bool isSearching = false;

private bool useLocalPatrol = false;
private Vector3 localpatrolCenter;

private Rigidbody2D rb;

protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
       
       if (patrolPointA && patrolPointB)
       {
        currentTarget = patrolPointB.position;
       }
    }

public override void Tick()
    {
        if (enemy.target)
        {
            isSearching = false;
            searchTimer = 0f;

            MoveTo(enemy.target.position);
            return;
        }
        if (!isSearching && enemy.lastKnownTargetPosition != Vector3.zero)
        {
            isSearching = true;
            searchTimer = searchTime;
            currentTarget = enemy.lastKnownTargetPosition;
            return;
        }
        if (isSearching)
        {
            searchTimer -= Time.deltaTime;
            SearchArea();
        
        if (searchTimer <= 0f)
            {
                isSearching = false;
                enemy.lastKnownTargetPosition = Vector3.zero;
                currentTarget = Vector3.zero;
            }
            return;
        }
    
        
        Patrol();
        
    }

    private void SearchArea()
    {
        Vector3 direction = (currentTarget - transform.position).normalized;
        MoveTo(currentTarget);

        if (Vector3.Distance(transform.position, currentTarget) < 0.2f)
        {
            Vector2 random = Random.insideUnitCircle * searchRadius;
            currentTarget = enemy.lastKnownTargetPosition + new Vector3(random.x, 0, random.y);
        }
    }

    private void Patrol()
    {
        if (patrolPointA == null || patrolPointB == null)
        {
            if (currentTarget == Vector3.zero)
            {
            Vector2 random = Random.insideUnitCircle * searchRadius;
            currentTarget = transform.position + new Vector3(random.x, 0, random.y);
            }
            MoveTo(currentTarget);
            return;
        }
        float zoneRadius = Vector3.Distance(patrolPointA.position, patrolPointB.position) / 2f;
        Vector3 zoneCenter = (patrolPointA.position + patrolPointB.position) / 2f;

        if (!useLocalPatrol && Vector3.Distance(transform.position, zoneCenter) > zoneRadius)
        {
            useLocalPatrol = true;
            localpatrolCenter = transform.position;

            Vector2 random = Random.insideUnitCircle * searchRadius;
            currentTarget = localpatrolCenter + new Vector3(random.x, 0, random.y);
        }
        if (useLocalPatrol)
        {
            MoveTo(currentTarget);

            if (Vector3.Distance(transform.position, currentTarget) < 0.2f)
            {
                Vector2 random = Random.insideUnitCircle * searchRadius;
                currentTarget = localpatrolCenter + new Vector3(random.x, 0, random.y);
            }
            return;
        }
        if(currentTarget == Vector3.zero)
        currentTarget = patrolPointA.position;

        MoveTo(currentTarget);

        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = currentTarget == patrolPointA.position ? patrolPointB.position : patrolPointA.position;
        }
    }
    private void MoveTo(Vector3 target)
{
    Vector2 direction = ((Vector2)target - rb.position).normalized;
    Vector2 move = new Vector2(direction.x, 0) * patrolSpeed * Time.fixedDeltaTime;
    rb.MovePosition(rb.position + move);
}
}
