using UnityEngine;

public class MoverPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontal;
    public float speed;
    public float jumpForce;
    private bool Grounded;
    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;

        
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0.0f)
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        else if (horizontal > 0.0f)
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);

       

        animator.SetBool("running", horizontal != 0.0f);
        Debug.DrawRay(transform.position, Vector3.down * 2f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector3.down, 2f))
        {
            Grounded = true;
            animator.SetBool("jumping", false);
        }
        else
        {
            Grounded = false;
            animator.SetBool("jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && Grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("attack");
           
        }

        


    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    
}


