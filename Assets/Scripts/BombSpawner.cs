
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bombPrefab;

    public float minX = -9.75f;
    public float maxX = 9.71f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBomb), 2f, 4f);
    }

    void SpawnBomb()
    {
        float x = Random.Range(minX, maxX);

        Vector3 spawnPosition = new Vector3(x, 3f, 0f);

        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }
}
