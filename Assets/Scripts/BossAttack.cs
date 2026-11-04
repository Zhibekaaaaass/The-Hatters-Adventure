
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float projectileSpeed = 6f;
    public float attackInterval = 2f;

    [Header("Shot Settings")]
    public float spreadAngle = 35f;
    public float aimDownAngle = 15f;

    void Start()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Assign Projectile Prefab!");
            return;
        }

        ShootFan();
        InvokeRepeating(
            nameof(ShootFan),
            attackInterval,
            attackInterval
        );
    }

    void ShootFan()
    {
        // Shoots toward the right, slightly downward
        Vector2 forward = Quaternion.Euler(
            0f, 0f, -aimDownAngle
        ) * Vector2.left;

        // Four projectiles spread around the forward direction
        float[] angles =
        {
            -spreadAngle,
            -spreadAngle / 3f,
            spreadAngle / 3f,
            spreadAngle
        };

        foreach (float angle in angles)
        {
            Vector2 direction =
                Quaternion.Euler(0f, 0f, angle) * forward;

            GameObject projectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity
            );

            Rigidbody2D rb =
                projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0f;
                rb.linearVelocity = direction * projectileSpeed;
            }
            else
            {
                Debug.LogError(
                    "Projectile needs Rigidbody2D!"
                );
                Destroy(projectile);
            }
        }
    }
}
