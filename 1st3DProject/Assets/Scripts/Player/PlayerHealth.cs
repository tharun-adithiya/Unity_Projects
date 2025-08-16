using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static float playerHealth = 100f;
    private CharacterController m_movement;

    [SerializeField] private UIManager m_uiManager;
    [SerializeField] private Animator animator;

    //Knockback functions

    private Vector3 knockbackVelocity; // Current knockback velocity
    private float knockbackDuration = 0f; // Time left for knockback
    private bool isKnockedBack = false; // Whether the player is currently being knocked back
    [SerializeField] private AudioManager m_audioManger;

    [SerializeField] private float knockbackHorizontalForce = 5f; // Horizontal knockback force
    [SerializeField] private float knockbackVerticalForce = 10f; // Vertical knockback force
    [SerializeField] private float knockbackTime = 0.5f; // Duration of knockback

    private void Start()
    {
        
        m_movement = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<CharacterController>();
        animator=GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if (m_movement == null)
        {
            Debug.LogWarning("CharacterController not found");
        }
    }

    private void Update()
    {
        // Apply knockback if active
        if (knockbackDuration > 0)
        {
            gameObject.isStatic=true;
            // Move the character based on the knockback velocity
            m_movement.Move(knockbackVelocity * Time.deltaTime);

            // Apply gravity to the vertical component of the knockback
            knockbackVelocity.y += Physics.gravity.y * Time.deltaTime;

            // Reduce the knockback duration over time
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            // Reset knockback state when done
            if (isKnockedBack)
            {
                gameObject.isStatic = false;
                isKnockedBack = false;
            }
        }

        // Check if health is zero or below
        if (playerHealth <= 0f)
        {
            Debug.Log("Player health is drained");
            StartCoroutine(OnHealthZero());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Trigger knockback when colliding with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            m_audioManger.OnPlayerDamage();
            m_uiManager.OnRecieveDamage(ObjectMovement.damage);
            ApplyKnockback(collision.transform.position);
            playerHealth -= ObjectMovement.damage;
            Debug.Log($"Player Health: {playerHealth}");
        }
    }

    public void ApplyKnockback(Vector3 sourcePosition)
    {
        // Calculate knockback direction away from the source
        Vector3 knockbackDirection = (transform.position - sourcePosition).normalized;

        // Set knockback velocity with horizontal and vertical components
        knockbackVelocity = new Vector3(
            knockbackDirection.x * knockbackHorizontalForce,
            knockbackVerticalForce,
            knockbackDirection.z * knockbackHorizontalForce
        );
        
        // Set knockback duration and activate knockback state
        knockbackDuration = knockbackTime;
        isKnockedBack = true;
    }

    private IEnumerator OnHealthZero()
    {
        Debug.Log("Health is zero");
        playerHealth = 100f;

        yield return new WaitForSeconds(0.5f);
        m_uiManager.OnGameOver();
    }
}
