using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Config")]
    public CharacterController controller;
    public Animator animator;
    [SerializeField] private Transform m_meshTransform;
    [SerializeField] private UIManager m_uiManager;
    [SerializeField] private PlayerInventory m_inventory;

    [Header("Movement Settings")]
    public float playerSpeed = 5f;
    [SerializeField] private float m_jumpSpeed = 5f;
    [SerializeField] private float m_gravity = -9.81f;
    [SerializeField] private Transform m_groundChecker;
    [SerializeField] private Transform m_groundCheckerForAudio;
    [SerializeField] private float m_checkRadius = 0.2f;
    [SerializeField] private float m_footSoundCheckRadius = 0.2f;
    [SerializeField] private LayerMask m_ground;

    [Header("Camera Settings")]
    [SerializeField] private Transform cam;

    private bool m_isPlayerInTrigger=false;
    private bool m_isPlayerInLanding = false;
    [HideInInspector] public bool m_isPlayerInCarRadius;
    
    private Vector3 m_inputDir;
    private Vector3 m_velocity;
    private float m_moveX;
    private float m_moveZ;
    private float playerRotator;
    private float targetAngle;
    public float turnDamping=0.5f;
    
    
    void Update()
    {

        HandleMovement();
        if (IsGrounded())
        {
            animator.SetBool("IsGrounded", true);

            StartCoroutine(LandingCheck());
            
        }
        
        HandleJumpAndGravity();
        
    }

    private void HandleMovement()
    {
        

        m_moveX = Input.GetAxisRaw("Horizontal");
        m_moveZ = Input.GetAxisRaw("Vertical");
        playerRotator = Input.GetAxis("Horizontal")*Time.deltaTime;

        
        m_inputDir = new Vector3(m_moveX, 0f, m_moveZ).normalized;

      
        animator.SetFloat("InputMagnitude", m_inputDir.magnitude);

        Debug.Log(m_inputDir.magnitude);

        if (m_inputDir.magnitude >= 0.1f)
        {
            Debug.Log(m_inputDir.magnitude);


            targetAngle = Mathf.Atan2(m_inputDir.x, m_inputDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float smoothedAngle = targetAngle;
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
            m_meshTransform.rotation= Quaternion.Euler(0f, smoothedAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, smoothedAngle, 0f) * Vector3.forward;
           
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        }

    }

    public void HandleJumpAndGravity()
    {
        bool grounded = IsGrounded();
        float originalSpeed = playerSpeed;
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
                    JumpChecker();
                    animator.SetBool("IsJumping", true);
                    m_velocity.y = Mathf.Sqrt(-2 * m_jumpSpeed * m_gravity);
                    
                }
                else
                {
                    animator.SetBool("IsJumping", false);
                }
            }
            
        }
        if (IsGrounded())
        {
            playerSpeed = originalSpeed;
        }
        m_velocity.y += m_gravity * Time.deltaTime;
        controller.Move(m_velocity * Time.deltaTime);


    }

    public void JumpChecker()
    {
        animator.SetBool("IsFalling",true);
        if (!IsGrounded())
        {
            animator.SetBool("IsJumping", true);
            return;
        }
    }

    IEnumerator LandingCheck()
    {
        m_isPlayerInLanding = true;
        animator.SetBool("IsLanding", true);
        animator.SetBool("IsFalling", false);

        if (m_inputDir.magnitude >= 0.1f)               
        {
            yield return new WaitForSeconds(1f);                        //Setting up delay for falling to running animation
        }
        
        yield return new WaitForSeconds(0.1f);

        animator.SetBool("IsLanding", false);
        m_isPlayerInLanding=false;
        
        yield return null;
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(m_groundChecker.position, m_checkRadius, m_ground);
    }

    public bool IsGroundedCheckForAudio()
    {
        return Physics.CheckSphere(m_groundCheckerForAudio.position, m_checkRadius, m_ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_groundChecker.position, m_checkRadius);
        Gizmos.DrawWireSphere(m_groundCheckerForAudio.position, m_footSoundCheckRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            m_isPlayerInTrigger = true;
            var interact = other.GetComponent<IInteractionManager>();
            m_uiManager.OnEnterCollectibleZone(m_isPlayerInTrigger,other.gameObject);
            interact?.OnPassTrigger(other.name);
        }
        if (other.gameObject.CompareTag("Delorean")) 
        {
            m_isPlayerInTrigger = true;
            m_isPlayerInCarRadius = true;
            m_uiManager.OnEnterDeloreanInteraction(m_isPlayerInTrigger); 
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Player exits zone");
            m_isPlayerInTrigger=false;
            m_uiManager.OnEnterCollectibleZone(m_isPlayerInTrigger,other.gameObject);
        }
        if (other.gameObject.CompareTag("Delorean"))
        {
            m_isPlayerInTrigger = false;
            m_isPlayerInCarRadius = false;
            m_uiManager.OnEnterDeloreanInteraction(m_isPlayerInTrigger); 
        }
    }

}
