/*using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyMoveModulePr : EnemyModule
{

    private Rigidbody2D rb;
    LayerMask layerMask;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        layerMask = LayerMask.GetMask("Wall");
    }

    public override void Tick()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
         else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}*/
