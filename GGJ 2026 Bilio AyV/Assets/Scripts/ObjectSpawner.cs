using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] float spawnRate;
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] ObjectPooler objectPool;

    float currentSpawnTimer;

    void Start()
    {
        currentSpawnTimer = 0;
    }

    void Update()
    {
        currentSpawnTimer += Time.deltaTime;
        if(currentSpawnTimer >= spawnRate)
        {
            TrySpawnObject();
            currentSpawnTimer = 0;
        }
    }

    void TrySpawnObject()
    {
        Vector2 spawnPosition = RandomPosition();
        if(!Physics2D.OverlapCircle(spawnPosition, 2f, ~0))
        {
            objectPool.SpawnFromPool(spawnPosition);
        }
    }

    Vector3 RandomPosition()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Length)].position;
    }

}
