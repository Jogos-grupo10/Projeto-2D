using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject spike;
    public float attackCooldown = 3f;
    private float nextAttackTime = 0f;

    private Animator anim;
    private EnemyEntity movement;
    private bool useStrong = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponentInParent<EnemyEntity>();
        if (movement == null)
            Debug.LogError("EnemyEntity não encontrado! Verifique a hierarquia.", this);
    }

    void Update()
    {
        if (movement.target == null || movement.isAttacking) return;
    
        float distance = Vector2.Distance(transform.position, movement.target.position);
        
        if (Time.time >= nextAttackTime)
        {
            if (distance <= movement.stopDistance || distance >= 5f) 
            {
                ChooseAttack(distance);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void ChooseAttack(float distance)
    {
        movement.isAttacking = true;
        
        anim.ResetTrigger("EnemyAttack2");
        anim.ResetTrigger("EnemyAttack3");
        anim.ResetTrigger("EnemyAttack4");

        if (distance <= movement.stopDistance)
        {
            if (useStrong) anim.SetTrigger("EnemyAttack2");
            else anim.SetTrigger("EnemyAttack3");

            useStrong = !useStrong;
        }
        else
        {
            anim.SetTrigger("EnemyAttack4");
        }
    }


    public void CreateSpike()
    {
        if (movement.target == null) return;
        Vector3 spawnPos = new Vector3(movement.target.position.x, transform.position.y, 0);
        Instantiate(spike, spawnPos, Quaternion.identity);
    }
    
    public void FinishAttackEffect()
    {
        movement.isAttacking = false;
    }
}