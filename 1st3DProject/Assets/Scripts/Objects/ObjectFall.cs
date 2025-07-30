using System.Collections;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    private Transform _targetTransform;
    public GridGenerator m_generator;
    private Vector3 randomPosition;
    public float spawnRate=0.5f;
    public float spawnHeight = 25f;
    private ObjectPooler m_pooler;


    void Start()
    {
        _targetTransform=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_pooler = ObjectPooler.instance;
        StartCoroutine(SpawnObstacle(spawnRate));
    }

    void FixedUpdate()
    {
        randomPosition = new Vector3(m_generator.randomPosX, spawnHeight, m_generator.randomPosZ);
    }
    private IEnumerator SpawnObstacle(float spawnRate)
    {
        while (_targetTransform != null)
        {
            yield return new WaitForSeconds(spawnRate);
            
            m_pooler.SpawnFromPool("Asteroid2b",randomPosition,Quaternion.identity);
            ObjectMovement movement =m_pooler.GetComponent<ObjectMovement>();

        }
        
    }
}
