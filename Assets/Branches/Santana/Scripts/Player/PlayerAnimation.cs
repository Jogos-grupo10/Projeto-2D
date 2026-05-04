using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 direction = PlayerMovement.movement;
        anim.SetFloat("dirX", direction.x);
        anim.SetFloat("dirY", direction.y);
        anim.SetBool("Jump", PlayerMovement.jumping);
        anim.SetBool("Falling", !PlayerMovement.inGround);
    }
}
