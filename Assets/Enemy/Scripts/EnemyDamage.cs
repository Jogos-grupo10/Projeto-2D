using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Entity player = collision.GetComponent<Entity>();

            if (player != null)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                
                player.TakeDamage(damageAmount, direction);
            }
        }
    }
}