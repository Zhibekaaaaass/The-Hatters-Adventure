
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet hit: " + other.name);

        DestroyRock rock = other.GetComponentInParent<DestroyRock>();
        Bomb bomb = other.GetComponentInParent<Bomb>();
        HeartPickup heart = other.GetComponentInParent<HeartPickup>();

        if (rock != null)
        {
            Destroy(rock.gameObject);
            Destroy(gameObject);
        }
        else if (bomb != null)
        {
            Destroy(bomb.gameObject);
            Destroy(gameObject);
        }
        else if (heart != null)
        {
            Destroy(heart.gameObject);
            Destroy(gameObject);
        }
    }
}
