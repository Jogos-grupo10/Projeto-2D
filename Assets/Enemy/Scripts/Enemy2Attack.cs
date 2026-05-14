using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{
    public float attackCooldown = 3f;
    private float nextAttackTime = 0f;
    
    private Animator anim;
    private EnemyEntity movement;
    
    public GameObject projetilPrefab;
    public Transform pontoSaida;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<EnemyEntity>();
    }
    
    void Update()
    {
        if (movement.target == null || movement.isAttacking) return;
    
        float distance = Vector2.Distance(transform.position, movement.target.position);
    
        if (Time.time >= nextAttackTime && distance <= 5f)
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
    
    void Atirar()
    {
        if (projetilPrefab == null || pontoSaida == null) return;

        Vector2 direcao = (movement.target.position - transform.position).normalized;
        GameObject bala = Instantiate(projetilPrefab, pontoSaida.position, Quaternion.identity);
        bala.GetComponent<Projetil>().Inicializar(direcao);
    }
}