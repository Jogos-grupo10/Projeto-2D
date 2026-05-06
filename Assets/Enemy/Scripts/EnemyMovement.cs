using UnityEngine;

public class EnemyMovement : Entity 
{
    public Transform target;
    public float stopDistance = 1.5f; 
    
    [HideInInspector] public Vector2 movementDir;
    [HideInInspector] public bool isAttacking = false;

    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null || isAttacking) 
        {
            movementDir = Vector2.zero;
            return;
        }

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            movementDir = (target.position - transform.position).normalized;
            
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
            if (movementDir.x > 0.01f) transform.localScale = new Vector3(1, 1, 1);
            else if (movementDir.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            movementDir = Vector2.zero;
        }
    }
}