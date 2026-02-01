using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] int inmediateObjectAmount; //Objects that will be available inmediately upon game start
    [SerializeField] float spawnRate;
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] ObjectPooler objectPool;
    [SerializeField] GameEvent newObjectSpawnedSender; //Contestants MUST KNOW!!!11

    float currentSpawnTimer;

    void Start()
    {
        currentSpawnTimer = 0;
        for(int i = 0; i < inmediateObjectAmount; i++)
        {
            TrySpawnObject();
        }
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
            newObjectSpawnedSender.TriggerEvent();
        }
    }

    Vector3 RandomPosition()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Length)].position;
    }

}
