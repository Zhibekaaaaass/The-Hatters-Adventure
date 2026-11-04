using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime = 0.3f;
    public float growSpeed = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
    }
}