using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPooler : MonoBehaviour
{
    //Use this script for instantiating objects through the pool design pattern
    [SerializeField] GameObject[] objectsToSpawn;

    [SerializeField] int poolSize;
    private Queue<GameObject> pool;
    private int currentObjectToCreate;

    protected virtual void Start()
    {
        pool = new Queue<GameObject>();
        FillPool();
    }

    public int GetPoolSize()
    {
        return poolSize;
    }

    void CreateObject()
    {
        GameObject obj = Instantiate(NextObjectToCreate(), transform.position, Quaternion.identity, transform); //obj, pos, rot, parent
        currentObjectToCreate++;
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public void FillPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateObject();
        }
    }

    //Cycle through objects to spawn on pool Creation
    GameObject NextObjectToCreate()
    {
        if(currentObjectToCreate >= objectsToSpawn.Length)
        {
            currentObjectToCreate = 0;
        }
        return objectsToSpawn[currentObjectToCreate];
    }
    public GameObject SpawnFromPool(Vector3 position)
    {
        GameObject spawnedObject = pool.Dequeue();

        spawnedObject.transform.position = position;
        spawnedObject.transform.rotation = Quaternion.identity;

        spawnedObject.SetActive(true);

        //enqueue next inactive object, if there aren't any create one
        EnqueueInactive();

        return spawnedObject;
    }



    void EnqueueInactive()
    {
        if (!pool.Any())
        {
            GameObject nextInactive = GetInactive();
            pool.Enqueue(nextInactive);
        }
    }

    GameObject GetInactive()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy)
                return child.gameObject;
        }
        //If there aren't any inactive children, send the first one
        GameObject newInactive = transform.GetChild(0).gameObject;
        newInactive.SetActive(false);
        return newInactive;
    }
}
