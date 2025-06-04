using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Vector3 _ballMotion;
    public GameObject ball;
    [SerializeField]private Transform _target;
    [SerializeField]private float _fallSpeed=5f;
    private Rigidbody _rb;
    private bool m_isStopFollowing=false;
    public Material ballMaterial;
   // [SerializeField] private float inAirTimer = 2f;   For Later Use
    Color ballAlpha;
    [SerializeField] private LayerMask m_groundLayer;
    private float m_groundCheckRadius=0.5f;
    [SerializeField] private Transform groundChecker;
    

    //private bool m_isAirTimeOver=false; For later Use
   
    void Start()
    {
        _target=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb=GetComponent<Rigidbody>();
        ballAlpha=ballMaterial.color;
       // ballAlpha.a = 50;
    }

    
    void Update()
    {
        
        if (_target == null)
        {
            Debug.Log("Object not found");
        }

       
        /*if (!isGrounded())        For later use
        {
            Debug.Log("Object not in ground");
            StartCoroutine(CheckDistance());
            
        }*/

        if (!m_isStopFollowing)
        {
            ObjectBehavior(); 
        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("ObjectCollided");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isStopFollowing = true;
            //StartCoroutine(BallDisappear());
            //Destroy(gameObject);
        }
    }

    private void ObjectBehavior()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _fallSpeed * Time.deltaTime);
    }

    /*private IEnumerator CheckDistance()           For later use
    {
        Debug.Log("Hello from Coroutine");
        
        Debug.Log(m_isAirTimeOver);
        yield return new WaitForSeconds(inAirTimer);
        if (m_isAirTimeOver == false)
        {
            m_isAirTimeOver = true;
        }
        
    }*/
    private bool isGrounded()
    {
        return Physics.CheckSphere(groundChecker.position, m_groundCheckRadius, m_groundLayer);
    }
    /*private IEnumerator BallDisappear()    For later use
    {

        for (int i = 0; i < 3; i++)
        {
            ballAlpha.a = 50;
            
            ballAlpha.a = 100;
            
        }
        yield return null;
        
    }*/
}
