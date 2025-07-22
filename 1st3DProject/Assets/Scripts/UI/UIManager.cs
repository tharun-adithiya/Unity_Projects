using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private Button m_restartButton;
    [SerializeField] private Button m_mainMenuButton;
    [SerializeField] private GameObject m_CollectText;
    [SerializeField] private RTXInteraction m_interactor;
    
    private bool m_isInZone=false;
    private GameObject inZoneObject;
    private void Update()
    {
        
        if (m_isInZone&&Input.GetKey(KeyCode.E))
        {
            callColect();
        }
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnEnterCollectibleZone(bool isInZone,GameObject inZoneObject)
    {
        if (isInZone)
        {
            m_isInZone = true;
            this.inZoneObject = inZoneObject;
            m_CollectText.SetActive(true);
           
        }
        else
        {
            m_isInZone = false;
            this.inZoneObject = null;
            m_CollectText.SetActive(false);
           
        }
    }
    public void callColect()
    {
        m_isInZone=false;
        Debug.Log("E is pressed");
        m_CollectText.SetActive(false);
            
        m_interactor.OnPressCollect(inZoneObject);
    }

}
