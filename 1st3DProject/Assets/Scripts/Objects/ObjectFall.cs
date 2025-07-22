using System.Collections;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    private Transform _targetTransform;
    //public ObjectMovement movement;
    public GridGenerator m_generator;
    private Vector3 randomPosition;
    [SerializeField]private float m_spawnRate=0.5f;
    [SerializeField] private float m_spawnHeight = 25f;
    private ObjectPooler m_pooler;


    void Start()
    {
        _targetTransform=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_pooler = ObjectPooler.instance;
        StartCoroutine(SpawnObstacle(m_spawnRate));
    }

    void FixedUpdate()
    {
        randomPosition = new Vector3(m_generator.randomPosX, m_spawnHeight, m_generator.randomPosZ);
    }
    private IEnumerator SpawnObstacle(float spawnRate)
    {
        while (_targetTransform != null)
        {
            yield return new WaitForSeconds(spawnRate);
            
            m_pooler.SpawnFromPool("Asteroid2b",randomPosition,Quaternion.identity);
            ObjectMovement movement =m_pooler.GetComponent<ObjectMovement>();
           // movement.Initialize(GameObject.Find("Player").transform);
            //Debug.Log("Xpos:" + randomPosition.x + ", Zpos:" + randomPosition.z);
        }
        
    }
}
