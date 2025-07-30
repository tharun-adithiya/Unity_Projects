using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private ObjectMovement movement;
    public Transform m_targetToFollow;
    [HideInInspector] public static ObjectPooler pooler;
    [SerializeField] private AsteroidRainStarter m_rainStarter;
    private Queue<GameObject> m_objectPool = new Queue<GameObject>();
    [System.Serializable]
    public class Pool
    {
        public string objectName;
        public GameObject prefab;
        public int poolSize;

    }
    #region Singleton
    public static ObjectPooler instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion


    [HideInInspector] public Dictionary<string, Queue<GameObject>> objectPoolDictionary;
    [SerializeField]private List<Pool> pools;
    void Start()
    {
        
        objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            //Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject poolObj = Instantiate(pool.prefab);
                poolObj.SetActive(false);
                
                m_objectPool.Enqueue(poolObj);
            }
            objectPoolDictionary.Add(pool.objectName, m_objectPool);
        }
    }
    public GameObject SpawnFromPool(string objName, Vector3 position, quaternion rotation)
    {
        
        if (!objectPoolDictionary.ContainsKey(objName))
        {
            Debug.LogWarning($"Object with name {objName} is not found");
        }

        GameObject objToSpawn=objectPoolDictionary[objName].Dequeue();

        objToSpawn.SetActive(true);

        objToSpawn.transform.SetPositionAndRotation(position, rotation);
        IPooledObject pooledObj=objToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            Debug.Log(m_targetToFollow);
            //movement.Initialize(m_targetToFollow);
            pooledObj.Initialize(m_targetToFollow);

             pooledObj.OnSpawnObject();

        }
        objectPoolDictionary[objName].Enqueue(objToSpawn);
        return objToSpawn;
    }

}
