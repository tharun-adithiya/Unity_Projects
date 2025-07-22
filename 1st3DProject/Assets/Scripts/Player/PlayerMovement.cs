using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Config")]
    public CharacterController controller;
    public Animator animator;
    [SerializeField] private Transform meshTransform;
    [SerializeField] private UIManager uiManager;

    [Header("Movement Settings")]
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private float m_dashSpeed = 5f;
    [SerializeField] private float m_jumpSpeed = 5f;
    [SerializeField] private float m_gravity = -9.81f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float m_checkRadius = 0.2f;
    [SerializeField] private LayerMask m_ground;
    [SerializeField] private AnimationClip m_LandingClip;
    [SerializeField] private AnimationClip m_JumpingClip;
    [Header("Camera and Audio Settings")]
    [SerializeField] private PlayerAudio m_footAudio;
    [SerializeField] private Transform cam;

    private bool m_isPlayerInTrigger=false;
    private bool m_isPlayerInLanding = false;

    private Vector3 m_inputDir;
    private Vector3 m_velocity;
    private float m_moveX;
    private float m_moveZ;
    private float playerRotator;
    private float targetAngle;
    public float turnDamping=0.5f;
    private float turnSmoothVelocity=0.5f;
    
    void Update()
    {
        if (IsGrounded())
        {
            animator.SetBool("IsGrounded", true);

            StartCoroutine(LandingCheck());
            
        }
        HandleMovement();
        StartCoroutine(HandleJumpAndGravity());
        
    }

    private void HandleMovement()
    {
        

        m_moveX = Input.GetAxisRaw("Horizontal");
        m_moveZ = Input.GetAxisRaw("Vertical");
        playerRotator = Input.GetAxis("Horizontal")*Time.deltaTime;

        
        m_inputDir = new Vector3(m_moveX, 0f, m_moveZ).normalized;

        //Debug.Log(m_inputDir.magnitude);
        animator.SetFloat("InputMagnitude", m_inputDir.magnitude);

       

        if (m_inputDir.magnitude >= 0.1f)
        {
            
            targetAngle = Mathf.Atan2(m_inputDir.x, m_inputDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float smoothedAngle = targetAngle;//Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnDamping);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
            meshTransform.rotation= Quaternion.Euler(0f, smoothedAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, smoothedAngle, 0f) * Vector3.forward;
           
            controller.Move(moveDir.normalized * m_speed * Time.deltaTime);
        }



        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.Move(m_inputDir * m_speed * m_dashSpeed * Time.deltaTime);
        }
    }

    IEnumerator  HandleJumpAndGravity()
    {
        bool grounded = IsGrounded();
        float originalSpeed = m_speed;
        if (!grounded)
        {
            
            animator.SetBool("IsGrounded", false);
            
            
            animator.SetBool("IsFalling", true);
        }
        else
        {
            
            animator.SetBool("IsFalling",false);
            if (m_velocity.y < 0f)
            {
                m_velocity.y = -2f;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("SpacePressed");
                    // float velocityInJumpState = m_speed / 1.5f;
                    //m_speed = velocityInJumpState;
                    
                    StartCoroutine(JumpChecker());
                    animator.SetBool("IsJumping", true);
                    m_velocity.y = Mathf.Sqrt(-2 * m_jumpSpeed * m_gravity);
                    yield return null;
                }
                else
                {
                    animator.SetBool("IsJumping", false);
                }
            }
            
        }
        if (IsGrounded())
        {
            m_speed = originalSpeed;
        }
        m_velocity.y += m_gravity * Time.deltaTime;
        controller.Move(m_velocity * Time.deltaTime);
        /*if (grounded)
        {
            StartCoroutine(LandingCheck());
        }*/

    }

    IEnumerator JumpChecker()
    {
        //yield return new WaitForSeconds(m_JumpingClip.length);
        animator.SetBool("IsFalling",true);
        if (!IsGrounded())
        {
            animator.SetBool("IsJumping", true);
            yield return null;
        }
    }

    IEnumerator LandingCheck()
    {
        m_isPlayerInLanding = true;
        animator.SetBool("IsLanding", true);
        animator.SetBool("IsFalling", false);

        if (m_inputDir.magnitude >= 0.1f)
        {
            yield return new WaitForSeconds(1f);
        }
        
        yield return new WaitForSeconds(0.1f);

        animator.SetBool("IsLanding", false);
        m_isPlayerInLanding=false;
        
        yield return null;
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundChecker.position, m_checkRadius, m_ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundChecker.position, m_checkRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            m_isPlayerInTrigger = true;
            var interact = other.GetComponent<IInteractionManager>();
            uiManager.OnEnterCollectibleZone(m_isPlayerInTrigger,other.gameObject);
            interact?.OnPassTrigger(other.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Player exits zone");
            m_isPlayerInTrigger=false;
            uiManager.OnEnterCollectibleZone(m_isPlayerInTrigger,other.gameObject);

        }
    }

}
