using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TopText;
    [SerializeField] private TextMeshProUGUI m_InteractionText;


    [SerializeField] private RTXInteraction m_interactor;
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private PlayerInventory m_playerInventory;
    
    private bool m_isInZone=false;
    private bool m_canPressF;
    private GameObject inZoneObject;

    private void Start()
    {
        StartCoroutine(OnStart());
    }
    private void Update()
    {
        if (m_canPressF&& Input.GetKeyDown(KeyCode.F))
        {   
            Finish();    
        }

        if (m_isInZone&&Input.GetKey(KeyCode.E))
        {
            callColect();
        }
    }
    IEnumerator OnStart()
    {
        TopText.text = "Turn Back!";
        yield return new WaitForSeconds(5f);
        TopText.text = "";
    }
    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OnPassSafeZone(bool isInSafeZone)
    {
        StartCoroutine(OnPassSafeArea(isInSafeZone));
    }
    public void OnEnterCollectibleZone(bool isInZone,GameObject inZoneObject)
    {
        if (isInZone)
        {
            m_isInZone = true;
            this.inZoneObject = inZoneObject;
            m_InteractionText.text = "Press E to collect!";
        }
        else
        {
            m_isInZone = false;
            this.inZoneObject = null;
            m_InteractionText.text="";
           
        }
    }

    public void OnEnterDeloreanInteraction(bool isInInteractionZone)
    {

        if (isInInteractionZone && m_playerInventory.Inventory.Count >= 4)
        {
            m_InteractionText.text = "You have all the materials, Press f to escape";
            m_canPressF = true;
        }
        else if (isInInteractionZone)
        {
            m_InteractionText.text = "Collect all the materials to start the car";
            m_canPressF = false;
        }
        else
        {
            m_InteractionText.text = "";
            m_canPressF = false;
        }
    }

    IEnumerator OnPassSafeArea(bool isNotSafe)
    {
        if (isNotSafe)
        {
            TopText.text = "Collect all the objects before dying:)";
            yield return new WaitForSeconds(5f);
            TopText.text = "";
        }
        else
        {
            yield return null;
        }

    }

    public void callColect()
    {
        m_isInZone=false;
        Debug.Log("E is pressed");
        m_InteractionText.text = "";
        m_interactor.OnPressCollect(inZoneObject);
    }
    public void Finish()
    {
        Debug.Log("All materials are used");
    }

}
