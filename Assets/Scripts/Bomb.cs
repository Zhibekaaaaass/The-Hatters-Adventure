using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 2f;
    public GameObject explosionPrefab;

    void Start()
    {
        Invoke(nameof(Explode), 2f);
    }

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRadius
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Health playerHealth = hit.GetComponent<Health>();
                BombHazard bombHazard = GetComponent<BombHazard>();

                if (playerHealth != null && bombHazard != null)
                {
                    bombHazard.HitPlayer(playerHealth);
                }
            }
        }

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}