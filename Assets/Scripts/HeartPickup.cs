
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Heart trigger: " + other.name);

        if (collected || !other.CompareTag("Player"))
        {
            return;
        }

        Health playerHealth = other.GetComponent<Health>();
        Heart heart = GetComponent<Heart>();

        if (playerHealth != null && heart != null)
        {
            collected = true;
            heart.HitPlayer(playerHealth);
            Debug.Log("Heart collected!");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Player Health or Heart component is missing!");
        }
    }
}
