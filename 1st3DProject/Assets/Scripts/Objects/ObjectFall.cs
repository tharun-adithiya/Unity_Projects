using System.Collections;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    private Transform m_targetTransform;
    private AsteroidRainStarter m_rainStarter;
    public GridGenerator m_generator;
    private Vector3 randomPosition;
    public float spawnRate=0.5f;
    public float spawnHeight = 25f;
    private ObjectPooler m_pooler;
    private Transform m_diffTarget;


    void Start()
    {
        m_pooler = ObjectPooler.instance;
        m_rainStarter =GameObject.FindGameObjectWithTag("AsteroidTrigger").GetComponent<AsteroidRainStarter>();
        if (m_rainStarter == null)
        {
            Debug.LogWarning("Asteroid rain starter not assigned");
        }
        m_diffTarget = m_rainStarter.differentTarget;
    }

    void FixedUpdate()
    {
        randomPosition = new Vector3(m_generator.randomPosX, spawnHeight, m_generator.randomPosZ);
    }
    public void TargetSwitcher(bool isNotSafe)
    {
        if (isNotSafe)
        {
            Debug.Log("Target set to player");
            m_targetTransform = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Transform>();
            StartCoroutine(SpawnObstacle(spawnRate));
        }
        else 
        {
            Debug.Log("TargetSwitched");
            m_targetTransform = m_diffTarget;
        }
        
    }
    private IEnumerator SpawnObstacle(float spawnRate)
    {
        while (m_targetTransform != null&&m_targetTransform!=m_diffTarget)
        {
            yield return new WaitForSeconds(spawnRate);
            Debug.Log("Spawning from pool");
            m_pooler.SpawnFromPool("Asteroid2b",randomPosition,Quaternion.identity);
            ObjectMovement movement =m_pooler.GetComponent<ObjectMovement>();
        }
        
    }
}
