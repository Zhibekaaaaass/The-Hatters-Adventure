
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 1;

    private Rigidbody2D rb;
    private bool hitSomething;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = direction.normalized * speed;
        }
    }

    void Start()
    {
        if (rb == null)
        {
            Debug.LogError("Boss projectile needs Rigidbody2D!");
            return;
        }

        rb.gravityScale = 0f;
        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CheckHit(other);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckHit(collision.collider);
    }

    void CheckHit(Collider2D other)
    {
        if (hitSomething)
            return;

        Transform target = other.transform;

        if (!other.CompareTag("Player"))
        {
            if (other.transform.root.CompareTag("Player"))
                target = other.transform.root;
            else
                return;
        }

        hitSomething = true;

        Player2D player = target.GetComponent<Player2D>();

        if (player == null)
            player = target.GetComponentInParent<Player2D>();

        if (player != null)
        {
            player.SendMessage(
                "TakeDamage",
                damage,
                SendMessageOptions.DontRequireReceiver
            );

            Debug.Log("Boss projectile hit Player!");
        }
        else
        {
            Debug.LogError("Player2D component not found!");
        }

        Destroy(gameObject);
    }
}
