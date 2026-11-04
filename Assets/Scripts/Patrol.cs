using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private float startX;
    private int dir = 1;
    private SpriteRenderer sr;

    void Start()
    {
        startX = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);

        if (dir == 1 && transform.position.x >= startX + distance)
        {
            dir = -1;
        }
        else if (dir == -1 && transform.position.x <= startX - distance)
        {
            dir = 1;
        }

        if (sr != null)
        {
            sr.flipX = dir < 0;
        }
    }
}