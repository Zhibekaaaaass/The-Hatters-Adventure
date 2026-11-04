
using UnityEngine;

public class DestroyRock : MonoBehaviour
{
    private bool touchedGround = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !touchedGround)
        {
            touchedGround = true;
            Destroy(gameObject, 2f);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            Hazard hazard = GetComponent<Hazard>();

            if (playerHealth != null && hazard != null)
            {
                hazard.HitPlayer(playerHealth);
            }

            Destroy(gameObject);
        }
    }
}
