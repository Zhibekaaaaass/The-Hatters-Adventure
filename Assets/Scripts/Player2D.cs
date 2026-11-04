
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player2D : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 7f;
    public float jumpForce = 12f;

    [Header("Sounds")]
    public AudioSource coinSound;
    public AudioSource deathSound;
    public AudioSource winSound;
    public AudioSource jumpSound;

    [Header("Level")]
    public Animator gateAnimator;
    public string nextScene;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool onGround;
    private bool levelDone;
    private bool isDead;

    private Vector2 move;

    private int coins;
    private int coinsTotal;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (rb == null)
            Debug.LogError("Player needs Rigidbody2D!");
    }

    void Start()
    {
        coinsTotal = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("Coins in this level: " + coinsTotal);
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (rb == null || !onGround || isDead || levelDone)
            return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        onGround = false;

        if (jumpSound != null)
            jumpSound.Play();
    }

    public void OnFire()
    {
        if (isDead || levelDone)
            return;

        Shooter shooter = GetComponent<Shooter>();

        if (shooter != null)
            shooter.Shoot();
        else
            Debug.LogWarning("Shooter component not found!");
    }

    void Update()
    {
        if (isDead || levelDone)
            return;

        float x = move.x;

        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(x));
            anim.SetBool("Grounded", onGround);
        }

        if (sr != null && x != 0)
            sr.flipX = x < 0;
    }

    void FixedUpdate()
    {
        if (rb == null || isDead || levelDone)
            return;

        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead || levelDone)
            return;

        if (other.CompareTag("Coin"))
        {
            coins++;

            Debug.Log("Coins: " + coins + " / " + coinsTotal);

            if (coinSound != null)
                coinSound.Play();

            Destroy(other.gameObject);

            if (coins >= coinsTotal && gateAnimator != null)
                gateAnimator.SetBool("Open", true);

            return;
        }

        if (other.CompareTag("Exit"))
        {
            if (coins < coinsTotal)
            {
                Debug.Log("Collect all coins first! " + coins + " / " + coinsTotal);
                return;
            }

            levelDone = true;
            move = Vector2.zero;

            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            Debug.Log("You win!");

            if (winSound != null)
                winSound.Play();

            Invoke(nameof(LoadNextLevel), 1f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        CheckGround(other);

        if (!isDead && !levelDone && other.gameObject.CompareTag("Enemy"))
            Die();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        CheckGround(other);
    }

    void CheckGround(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    onGround = true;
                    return;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            onGround = false;
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        move = Vector2.zero;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        Debug.Log("Game over");

        if (deathSound != null)
            deathSound.Play();

        Invoke(nameof(RestartLevel), 0.5f);
    }

    void LoadNextLevel()
    {
        if (string.IsNullOrWhiteSpace(nextScene))
        {
            Debug.LogError("Set Next Scene in Player2D Inspector!");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(nextScene))
        {
            Debug.LogError("Scene not found in Build Profiles: " + nextScene);
            return;
        }

        SceneManager.LoadScene(nextScene);
    }

    void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (!string.IsNullOrWhiteSpace(currentScene))
            SceneManager.LoadScene(currentScene);
        else
            Debug.LogError("Current scene name is empty!");
    }
}
