using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public float attackCooldown = 3f;
    private float nextAttackTime = 0f;
    
    private Animator anim;
    private EnemyMovement movement;
    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
    }
    
    void Update()
    {
        if (movement.target == null || movement.isAttacking) return;
        
        float distance = Vector2.Distance(transform.position, movement.target.position);
        
        if (distance <= movement.stopDistance || distance >= 5f) 
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }
    
    void Attack()
    {
        movement.isAttacking = true;
        
        anim.ResetTrigger("Enemy2Attack");
        anim.SetTrigger("Enemy2Attack");
    }
    
    public void FinishAttackEffect()
    {
        movement.isAttacking = false;
    }
}
