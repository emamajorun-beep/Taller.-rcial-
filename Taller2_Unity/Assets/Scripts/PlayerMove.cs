using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontal;
    public float speed;
    public float jumpForce;
    private Vector3 initialScale;

    private bool recibiendoDanio;
    public float fuerzaRebote = 15f;

    private bool isGrounded;
    private int jumpCount;
    public int maxJumps = 2;

    private AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip fallSound;
    public AudioClip[] attackSounds;
    public AudioClip deathSound;

    public GameObject gameOverCanvas;
    public GameObject espada;

    private bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
        if (gameOverCanvas != null) gameOverCanvas.SetActive(false);
        isDead = false;

        if (espada != null)
            espada.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f)
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        else if (horizontal > 0.0f)
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);

        animator.SetBool("running", horizontal != 0.0f);

        if (isGrounded) jumpCount = 0;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
            jumpCount++;
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            if (attackSounds.Length > 0) PlayRandomSound(attackSounds);
            ActivarEspadaTemporal();
        }

        if (!isGrounded)
        {
            if (rb.linearVelocity.y > 1f)
            {
                animator.SetBool("jumping", true);
                animator.SetBool("falling", false);
            }
            else if (rb.linearVelocity.y < -1f)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
                PlaySound(fallSound);
            }
        }
        else
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
        }

        animator.SetBool("recibeDanio", recibiendoDanio);
    }

    public void RecibeDanio(Vector2 posicionEnemigo, int cantDanio)
    {
        if (!recibiendoDanio && !isDead)
        {
            recibiendoDanio = true;

            
            GameManager.Instance.AddLives(-cantDanio);

            
            Vector2 direccion = ((Vector2)transform.position - posicionEnemigo).normalized + Vector2.up * 0.5f;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direccion * fuerzaRebote, ForceMode2D.Impulse);

            
            animator.SetTrigger("recibeDanio");

            
            if (GameManager.Instance.playerLives <= 0)
            {
                Morir();
            }
            else
            {
                Invoke("DesactivaDanio", 0.4f);
            }
        }
    }

    private void Morir()
    {
        isDead = true;

        
        animator.SetTrigger("death");

        
        rb.linearVelocity = Vector2.zero;

        
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        
        this.enabled = false;

        
        PlaySound(deathSound);
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        PlaySound(jumpSound);
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        if (horizontal != 0 && isGrounded) PlaySound(walkSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && !isDead)
        {
            Morir();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && !audioSource.isPlaying)
            audioSource.PlayOneShot(clip);
    }

    private void PlayRandomSound(AudioClip[] clips)
    {
        if (clips.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, clips.Length);
            audioSource.PlayOneShot(clips[index]);
        }
    }

    public void Retry()
    {
        //---------se reinicia la vida al morir
        GameManager.Instance.playerLives = 4;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ActivarEspadaTemporal()
    {
        if (espada != null)
        {
            espada.SetActive(true);
            Invoke("DesactivarEspada", 0.2f);
        }
    }

    public void DesactivarEspada()
    {
        if (espada != null)
            espada.SetActive(false);
    }
}
