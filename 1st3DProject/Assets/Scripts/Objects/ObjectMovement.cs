using System.Collections;
using Unity.Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMovement : MonoBehaviour, IPooledObject
{
    public GameObject Asteroid;
    private Transform m_target;
    [SerializeField] private float m_fallSpeed = 5f;
    private Rigidbody m_rb;
    [HideInInspector]public bool m_isStopFollowing = false;
    private Transform m_asteroidTransform;
    [SerializeField] private LayerMask m_groundLayer;
    private float m_groundCheckRadius = 0.5f;
    [SerializeField] private Transform groundChecker;
    private CinemachineImpulseSource m_impulseSource;
    [SerializeField] private GameObject m_imapctParticles;
    
    //[SerializeField]private Transform m_imapctPosition;
    private Terrain m_terrain;
    private ObjectPooler m_pool;
    public static float damage = 25f;



    void Start()
    {
        m_pool = ObjectPooler.instance;
        m_terrain = Terrain.activeTerrain;
        m_asteroidTransform = gameObject.transform;
        m_impulseSource = GetComponent<CinemachineImpulseSource>();
        m_target = GameObject.Find("Player").transform;
        if (m_target != null)
        {
            Debug.Log("Target  found");
        }
        m_rb = GetComponent<Rigidbody>();

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
            //ImpactRadiusMarker(m_target);
            OnSpawnObject();
            m_isStopFollowing = false;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
        
        transform.position = Vector3.Lerp(transform.position, m_target.position, m_fallSpeed * Time.deltaTime);
    }

    /*public void ImpactRadiusMarker(Transform impactRadius)
    {
        if (impactRadius == null)
        {
            Debug.LogWarning("ImpactRadius is not assigned");
        }
        float terrainHeight = m_terrain.SampleHeight(impactRadius.position);
        Vector3 adjustedPositionForMarker = new Vector3(impactRadius.position.x, terrainHeight, impactRadius.position.z);
        m_pool.SpawnFromPool("AttackRadiusIndicator", adjustedPositionForMarker, quaternion.identity);
        return;
    }*/

    public IEnumerator OnHitGround(Vector3 effectPosition)
    {
        float terrainHeight = m_terrain.SampleHeight(effectPosition);
        Vector3 adjustedPosition = new Vector3(effectPosition.x, terrainHeight, effectPosition.z);
        m_pool.SpawnFromPool("ImpactVFX", adjustedPosition, quaternion.identity);                   //Implementation of objectPool to VFX.
        //var hitEffect = Instantiate(m_imapctParticles,adjustedPosition,Quaternion.identity);     //This Line of Code is used without implementation of object pool.
        yield return null;
        // GameObject.Destroy(hitEffect);                                                         //Used along with Instantiate when object pool is not implemented.  
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundChecker.position, m_groundCheckRadius, m_groundLayer);
    }

}
