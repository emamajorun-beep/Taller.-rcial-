using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 4.0f;

    public int vida = 3;
    private int vidaActual;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private bool recibiendoDanio;
    private Animator animator;
    private Vector3 initialScale;

    public float fuerzaRebote = 5f; 
    public float alturaRebote = 2f; 

    private bool estaMuerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;
        vidaActual = vida;

        if (rb != null)
        {
            rb.gravityScale = 1f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        if (player == null || animator == null || estaMuerto) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius && !recibiendoDanio)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x < 0)
                transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            else if (direction.x > 0)
                transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);

            movement = new Vector2(direction.x, 0);
            enMovimiento = true;

            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
        else
        {
            movement = Vector2.zero;
            enMovimiento = false;
        }

        animator.SetBool("enMovimiento", enMovimiento);
        animator.SetBool("muerto", estaMuerto); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var mover = collision.gameObject.GetComponent<MoverPlayer>();
            if (mover != null)
                mover.RecibeDanio(transform.position, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
            Vector2 direccionDanio = (transform.position - collision.transform.position).normalized;
            direccionDanio.y = alturaRebote;
            RecibeDanio(1, direccionDanio);
        }
    }

    public void RecibeDanio(int cantDanio, Vector2 direccion)
    {
        if (estaMuerto || recibiendoDanio) return;

        vida -= cantDanio;
        recibiendoDanio = true;
        animator.SetTrigger("hit_enemy");

        //----Empuje espadita
        Vector2 knockback = new Vector2(direccion.x * fuerzaRebote, alturaRebote);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockback, ForceMode2D.Impulse);

        if (vida <= 0)
        {
            Morir();
        }
        else
        {
            StartCoroutine(DesactivaDanio());
        }
    }

    private IEnumerator DesactivaDanio()
    {
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
    }

    private void Morir()
    {
        estaMuerto = true;

        
        animator.SetBool("muerto", true);

       
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 

        
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;

        
        this.enabled = false;

        
    }
}
