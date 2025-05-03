using UnityEngine;

public class PlayerController : MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool facingRight = true;
    public bool isGrounded = false;
    public bool jumping = false;
    public bool doubleJump = false;// pode pular duas vezes
    public bool doubleJumping = false;// está pulando duas vezes
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){

        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);

        float h = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }

        
        // Pula se o jogador estiver no chão e a tecla de pulo for pressionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                doubleJump = true;
                jumping = true;
            } else if (doubleJump)
            {
                doubleJumping = true;
                doubleJump = false;
            }
            
        }
        rb.linearVelocity = new Vector2(h * speed, rb.linearVelocity.y);



        // rb.linearVelocity = new Vector2 (h * speed, v * speed);//desligar a gravidade, movimentação para top down

        //rb.AddForce(new Vector2(h, y));
        /*if (h != 0){
            transform.position = new Vector3(transform.position.x + h * speed,
                transform.position.y, transform.position.z);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.position = new Vector3(transform.position.x,
                transform.position.y + y * speed, transform.position.z);
        }
        */
    }

    void Flip() {
        // Inverte a direção que o jogador está enfrentando
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            jumping = false;
        }

        if(doubleJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce));
            doubleJumping = false;
        }
    }

}
