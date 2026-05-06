using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    private GameObject spike;
    private GameObject enemy;
    public int health;
    void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        Animator enemyAnim = enemy.GetComponent<Animator>();
        
        if (enemyAnim != null)
        {
            enemyAnim.SetTrigger("Hurt");
        }

        if (health <= 0)
        {
            enemyAnim.SetTrigger("Death");
            Destroy(gameObject);
        }
    }
    
}
