using UnityEngine;

public class EnemyEntity : Entity
{
    public Transform target;
    public float stopDistance = 1.5f;
    
    public Vector2 movementDir;
    public bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = maxHealth;
        invincibilityDuration = 0f;
        animator = GetComponent<Animator>();

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
            rb.linearVelocity = new Vector2(movementDir.x * speed, rb.linearVelocity.y);

            if (movementDir.x > 0.01f) transform.localScale = new Vector3(0.5f, 0.5f, 1);
            else if (movementDir.x < -0.01f) transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
        else
        {
            movementDir = Vector2.zero;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    public override void TakeDamage(int damage, Vector2 direction)
    {
        base.TakeDamage(damage, direction);

        if (animator != null)
            animator.SetTrigger("Hurt");
    }

    protected override void Die()
    {
        if (animator != null)
            animator.SetTrigger("Death");

        Destroy(gameObject, 0.5f);
    }
}