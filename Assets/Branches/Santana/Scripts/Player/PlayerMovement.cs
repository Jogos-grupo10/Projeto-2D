using UnityEngine;

public class PlayerMovement : Entity
{
    public float attackRange = 1f;
    public float attackOffset = 0.5f;

    public static Vector2 movement;
    public static bool jumping;
    public static bool inGround;

    public int jumpForce = 5;

    void Start()
    {
        jumping = false;
        inGround = true;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector2(moveX, moveY);

        Move(movement); 

        if (Input.GetButtonDown("Jump") && inGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumping = true;
            inGround = false;
        }

        if (Input.GetMouseButtonDown(0) && attackCooldown.ElapsedTimeSec() > 0.5f)
        {
            Debug.Log("ok");
            animator.SetTrigger("Attack");
            attackCooldown.Restart();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            inGround = true;
            jumping = false;
        }
    }

    public void CastAttackHitbox()
    {
        Vector2 hitboxPos = (Vector2)transform.position + new Vector2(transform.localScale.x * attackOffset, 0);
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitboxPos, attackRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Entity entity) && entity != this)
            {
                entity.TakeDamage(damage, (entity.transform.position - transform.position).normalized);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 hitboxPos = (Vector2)transform.position + new Vector2(transform.localScale.x * attackOffset, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitboxPos, attackRange);
    }
}