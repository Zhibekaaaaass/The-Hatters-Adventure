
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject hazardPrefab;

    public float minX = -9.75f;
    public float maxX = 9.71f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, 2f);
    }

    void Spawn()
    {
        float x = Random.Range(minX, maxX);

        Vector3 spawnPosition = new Vector3(x, 3f, 0f);

        Debug.Log("Rock spawned at X = " + x);

        Instantiate(hazardPrefab, spawnPosition, Quaternion.identity);
    }

}
