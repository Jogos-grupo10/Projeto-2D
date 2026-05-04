using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int speed;
    private Rigidbody2D player;
    public static Vector2 movement;
    public static bool jumping ;
    public static bool inGround;
    public int jumpForce;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        speed = 5;
        jumping = false;
        inGround = true;
        jumpForce = 5;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        player.MovePosition(player.position + movement * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && inGround)
        {
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumping = true;
            inGround = false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Colidiu com: " + col.gameObject.tag);
        if (col.gameObject.CompareTag("Ground"))
        {
            inGround = true;
            jumping = false;
        }
    }
}
