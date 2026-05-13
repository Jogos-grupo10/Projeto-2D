using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    private Animator enemyAnim;

    void Start()
    {
        health = maxHealth;
        enemyAnim = GetComponent<Animator>(); 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }

        else
        {
            enemyAnim.SetTrigger("Hurt");
        }

        
    }

    private void Die()
    {
        if (enemyAnim != null)
        {
            enemyAnim.SetTrigger("Death");
        }

        Destroy(gameObject); 
    }
}