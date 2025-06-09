using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static float playerHealth=100f;
    [SerializeField]private GameOverScene scene;

    private void Update()
    {
        if (playerHealth <=0f)
        {
            Debug.Log("Player health is drained");
            StartCoroutine(OnHealthZero());
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerHealth-=ObjectMovement.damage;
            Debug.Log(playerHealth);
        }
    }

    IEnumerator  OnHealthZero()
    {
        Debug.Log("Health is zero");
        playerHealth = 100f;
        //Destroy(gameObject);
        
        yield return new WaitForSeconds(1f);
        scene.GameOver();
        //SceneManager.LoadScene(0);

    }
}
