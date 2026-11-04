
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = transform.right * 12f;
        }

        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player bullet damages the boss.
        if (other.CompareTag("Boss"))
        {
            BossHealth boss = other.GetComponent<BossHealth>();

            if (boss == null)
                boss = other.GetComponentInParent<BossHealth>();

            if (boss != null)
            {
                boss.TakeDamage(1);
                Debug.Log("Boss hit! HP: " + boss.currentHealth);
            }
            else
            {
                Debug.LogWarning("BossHealth component not found!");
            }

            Destroy(gameObject);
            return;
        }

        // Keep the existing rock and bomb behavior.
        Hazard hazard = other.GetComponent<Hazard>();

        if (hazard != null)
        {
            if (hazard is Bomb && explosionPrefab != null)
            {
                Instantiate(
                    explosionPrefab,
                    other.transform.position,
                    Quaternion.identity
                );
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
