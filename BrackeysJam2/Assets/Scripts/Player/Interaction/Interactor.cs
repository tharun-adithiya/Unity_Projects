using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private PowerUpUIManager m_powerUpUIManager;
    [SerializeField] private GameObject powerUpPanel;
    private bool m_isInInteractionZone = false;
    private void Update()
    {
        if (m_isInInteractionZone&&Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(m_powerUpUIManager.OpenPowerUpPanel());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SelectionBoard"))
        {
            m_isInInteractionZone = true;
            Debug.Log($"IsInInteractionZone?:{m_isInInteractionZone}");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SelectionBoard"))
        {
            m_isInInteractionZone = false;
            Debug.Log($"IsInInteractionZone?:{m_isInInteractionZone}");
        }
    }
}
