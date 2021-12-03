using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Unity tutorial on object pooling
 * https://learn.unity.com/tutorial/introduction-to-object-pooling#
 * 
 * Brackeys - OBJECT POOLING in Unity
 * https://www.youtube.com/watch?v=tdSmKaJvCoA
 * 
 */

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler SharedInstance;
    public Queue<GameObject> pooledObjects;

    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    public interface IPooledObject
    {
        public void OnObjectSpawn();
    }


    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new Queue<GameObject>();

        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Enqueue(tmp);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        
        GameObject objectToSpawn = pooledObjects.Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }

        pooledObjects.Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}