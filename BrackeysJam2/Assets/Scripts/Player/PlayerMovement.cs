using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private float moveX;
    private float moveY;
    private Vector2 m_direction;
    public float moveSpeed=5f;
    
    private bool m_canDash = true;
    private bool m_isDashing=false;
    [HideInInspector]public bool isInInteractionRange = false;
    [SerializeField] private float m_dashSpeed=10;
    [SerializeField] private float m_dashingTime = 0.3f;
    [SerializeField]private float m_dashCoolDown=0.5f;
    public bool isMovingInNegative;

    [SerializeField] private PowerUpSwitcher m_powerUpProperties;
    [SerializeField] private PowerUpUIManager m_powerUpUIManager;
    [SerializeField] private Animator m_animator;
    private const string m_lastHorizontalInp = "LastHorizontal";
    private const string m_lastVerticalInp = "LastVertical";

    void Start()
    {
        m_rb=GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        m_animator.SetFloat("Horizontal", moveX);
        m_animator.SetFloat("Vertical", moveY);

        if (moveX < 0f)
        {
            isMovingInNegative = true;
        }
        else if(moveX>0f)
        {
            isMovingInNegative = false;
        }
        PlayerSideAnimations();
        if (m_powerUpProperties.isUsingSpeedPowerUp)
        {
            Debug.Log("Using SpeedPowerUp");
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(SpeedPowerUp());
            }
        }
        if (m_powerUpProperties.isUsingDashPowerUp&&m_powerUpProperties.dashCount>0)
        {
            Debug.Log("Using DashPowerUp");
            if (Input.GetKeyDown(KeyCode.LeftShift)&&m_canDash)
            {
                StartCoroutine(DashPowerUp());
            }
        }
    }
    private void FixedUpdate()
    {
        if(m_isDashing)
        {
            return;
        }
        Movement();
    }
    private void Movement()
    {
        m_direction=new Vector2(moveX,moveY).normalized;
        
        m_rb.linearVelocity=m_direction*moveSpeed;

    }
    IEnumerator DashPowerUp()
    {
        m_canDash = false;
        m_isDashing = true;
        Debug.Log("Dashing");
        m_direction = new Vector2(moveX, moveY).normalized;
        m_rb.linearVelocity=m_direction*m_powerUpProperties.dashPower;
        yield return new WaitForSeconds(m_dashingTime);
        Debug.Log("DashComplete");
        --m_powerUpProperties.dashCount;
        m_isDashing=false;
        yield return new WaitForSeconds(m_dashCoolDown);
        m_canDash=true;
        Debug.Log("DashCoolDownComplete");
    }
    IEnumerator SpeedPowerUp()
    {
        var oldSpeed = moveSpeed;
        moveSpeed += m_powerUpProperties.speedUpPower;
        Debug.Log("Speed Increased");
        yield return new WaitForSeconds(m_powerUpProperties.speedUpDuration);
        moveSpeed = oldSpeed;
        Debug.Log("Speed Decreased");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoorInteractor"))
        {
            Debug.Log("In range");
            isInInteractionRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoorInteractor"))
        {
            Debug.Log("Out of range");
            isInInteractionRange =false;
        }

    }

    public void PlayerSideAnimations()
    {
        if (m_direction != Vector2.zero)
        {
            Debug.Log("Setting Idle animation");
           // Debug.Log($"MoveX: {moveX}");
            m_animator.SetFloat(m_lastHorizontalInp, moveX);
          // Debug.Log($"MoveY: {moveY}");
            m_animator.SetFloat(m_lastVerticalInp, moveY);
            
        }
        if (m_direction.sqrMagnitude > 0.01f)
        {
            float directionMagnitude = m_direction.sqrMagnitude;
            m_animator.SetFloat("InputMagnitude", directionMagnitude);
        }
        else
        {
            m_animator.SetFloat("InputMagnitude", 0f);
        }

    }

}
