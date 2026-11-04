
using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab;

    public float minX = -9.75f;
    public float maxX = 9.71f;

    private GameObject currentHeart;

    void Start()
    {
        InvokeRepeating(nameof(SpawnHeart), 5f, 12f);
    }

    void SpawnHeart()
    {
        if (currentHeart != null)
        {
            Destroy(currentHeart);
        }

        float x = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(x, 3f, 0f);

        currentHeart = Instantiate(
            heartPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
