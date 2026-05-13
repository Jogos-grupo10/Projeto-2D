using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 20f;
    public float dano = 10f;
    private Vector2 direcao;

    public void Inicializar(Vector2 dir)
    {
        direcao = dir.normalized;
        float angulo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Entity entity = other.GetComponent<Entity>();
    
        if (entity != null && other.tag == "Player")
        {
            direcao = (other.transform.position - transform.position).normalized;
            entity.TakeDamage((int)dano, direcao);
            Destroy(gameObject);
        }
    }
}