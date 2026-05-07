using System.Collections;
using UnityEngine;
using Paradoxical.Core;

public class Entity : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public int health = 100;
    public int damage = 10;
    public float speed = 5f;
    public float maxSpeed = 10f;
    public float friction = 0.9f;
    public float invincibilityDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float knockbackForce = 5f;
    protected CustomStopwatch attackCooldown, damageCooldown;
    private Vector2 knockbackVelocity;

    private void Awake()
    {
        attackCooldown = new CustomStopwatch();
        damageCooldown = new CustomStopwatch();
        attackCooldown.Restart();
        damageCooldown.Restart();
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (damageCooldown.ElapsedTimeSec() < invincibilityDuration) return;
        damageCooldown.Restart();
        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake(shakeMagnitude);
        health -= damage;
        StartCoroutine(DamageAnim());
        Knockback(direction, damage * knockbackForce);
        if (health <= 0) Die();
    }

    private IEnumerator DamageAnim()
    {
        Time.timeScale = 0.1f;
        spriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
        spriteRenderer.color = Color.white;
    }

    public void Knockback(Vector2 direction, float force)
    {
        knockbackVelocity = direction.normalized * force;
    }

    private void Die()
    {
        Destroy(gameObject, 0.1f);
    }

    public void Move(Vector2 direction)
    {
        knockbackVelocity *= friction;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x * friction, rb.linearVelocity.y);
    
        rb.linearVelocity += new Vector2(direction.x * speed * Time.deltaTime, 0);
    
        float clampedX = Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed);
        rb.linearVelocity = new Vector2(clampedX, rb.linearVelocity.y);
    
        rb.linearVelocity += new Vector2(knockbackVelocity.x * Time.deltaTime, 0);

        if (Mathf.Abs(direction.x) > 0.01f)
            transform.localScale = new Vector3(direction.x < 0 ? -1 : 1, 1, 1);
    }
}