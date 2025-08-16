using System.Collections;
using Unity.Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMovement : MonoBehaviour, IPooledObject
{

    private Transform m_target;
    public float fallSpeed = 5f;
    [HideInInspector] public bool m_isStopFollowing = false;
    private CinemachineImpulseSource m_impulseSource;
    [SerializeField] private GameObject m_imapctParticles;

    private Terrain m_terrain;
    private ObjectPooler m_pool;
    public static float damage = 25f;



    void Start()
    {
        m_pool = ObjectPooler.instance;
        m_terrain = Terrain.activeTerrain;
        m_impulseSource = GetComponent<CinemachineImpulseSource>();
        m_target = GameObject.Find("Player").transform;
        if (m_target != null)
        {
            Debug.Log("Target  found");
        }

    }

    public void Initialize(Transform target)
    {
        m_target = target;
        m_isStopFollowing = false;
        Debug.Log("Object initialized with target: " + target?.name);
    }
    void Update()
    {

        if (!m_isStopFollowing)
        {
            OnSpawnObject();
            m_isStopFollowing = false;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBody"))
        {
            print("ObjectCollided");
            m_isStopFollowing = true;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            CameraShakeManager.instance.cameraShake(m_impulseSource);
            var hitPosition = collision.contacts[0].point;
            StartCoroutine(OnHitGround(hitPosition));
            m_isStopFollowing = true;

        }
    }

    public void OnSpawnObject()
    {
        transform.position = Vector3.Lerp(transform.position, m_target.position, fallSpeed * Time.deltaTime);
    }

    public IEnumerator OnHitGround(Vector3 effectPosition)
    {
        float terrainHeight = m_terrain.SampleHeight(effectPosition);
        Vector3 adjustedPosition = new Vector3(effectPosition.x, terrainHeight, effectPosition.z);
        m_pool.SpawnFromPool("ImpactVFX", adjustedPosition, quaternion.identity);                   //Implementation of objectPool to VFX.
        //var hitEffect = Instantiate(m_imapctParticles,adjustedPosition,Quaternion.identity);     //This Line of Code is used without implementation of object pool.
        yield return null;
        // GameObject.Destroy(hitEffect);                                                         //Used along with Instantiate when object pool is not implemented.  
    }

}
