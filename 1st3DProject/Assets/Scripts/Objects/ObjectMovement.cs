using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMovement : MonoBehaviour,IPooledObject
{
    private Vector3 _ballMotion;
    public GameObject ball;
    [HideInInspector]private Transform m_target;
    [SerializeField]private float _fallSpeed=5f;
    private Rigidbody _rb;
    private bool m_isStopFollowing=false;
    [SerializeField] private LayerMask m_groundLayer;
    private float m_groundCheckRadius=0.5f;
    [SerializeField] private Transform groundChecker;
    public static float damage = 25f;

    
   
    void Start()
    {
        m_target = GameObject.Find("Player").transform;
        if (m_target != null)
        {
            Debug.Log("Target  found");
        }
        _rb =GetComponent<Rigidbody>();

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
        if (collision.gameObject.CompareTag("Player"))
        {
            print("ObjectCollided");
            m_isStopFollowing = true;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
           
            m_isStopFollowing = true;
            
            //Destroy(gameObject);
        }
    }

    public void OnSpawnObject()
    {
        m_target = GameObject.Find("Player").transform;

        if (m_target == null)
        {
            Debug.LogWarning("Target not found");
        }
        transform.position = Vector3.Lerp(transform.position, m_target.position, _fallSpeed * Time.deltaTime);
    }

    private bool isGrounded()
    {
        return Physics.CheckSphere(groundChecker.position, m_groundCheckRadius, m_groundLayer);
    }

}
