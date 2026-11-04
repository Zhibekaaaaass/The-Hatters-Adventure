
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 3f;

    private Vector2 direction = Vector2.right;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
        Debug.Log("Bullet direction: " + direction);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        DestroyRock rock = other.GetComponentInParent<DestroyRock>();
        Bomb bomb = other.GetComponentInParent<Bomb>();
        HeartPickup heart = other.GetComponentInParent<HeartPickup>();

        if (rock != null || bomb != null || heart != null)
        {
            if (rock != null)
                Destroy(rock.gameObject);
            else if (bomb != null)
                Destroy(bomb.gameObject);
            else
                Destroy(heart.gameObject);

            Destroy(gameObject);
        }
    }
}
