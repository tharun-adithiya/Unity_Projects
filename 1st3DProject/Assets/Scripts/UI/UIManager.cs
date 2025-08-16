using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TopText;
    [SerializeField] private TextMeshProUGUI m_InteractionText;
    [SerializeField] private GameObject m_gameOverPanel;
    [SerializeField] private GameObject m_gameFinishedPanel;
    [SerializeField] private Image m_healthFill;

    [SerializeField] private RTXInteraction m_interactor;
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private PlayerInventory m_playerInventory;
    [SerializeField] private AudioManager m_audioManager;
    
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
            StartCoroutine((OnGameFinish()));    
        }

        if (m_isInZone&&Input.GetKey(KeyCode.E))
        {
            CallColect();
        }
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    IEnumerator OnStart()
    {
        TopText.text = "Turn Back!";
        yield return new WaitForSeconds(5f);
        TopText.text = "";
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
            TopText.text = "Collect all the materials before dying:)";
            yield return new WaitForSeconds(5f);
            TopText.text = "";
        }
        else
        {
            yield return null;
        }

    }

    public void OnRecieveDamage(float damage)
    {
        if (m_healthFill.fillAmount >= 0f)
        {
            damage = 10/damage;
            damage = (float)Math.Round(damage,1);
            Debug.Log($"Damage={damage}");
            m_healthFill.fillAmount -=damage;
        }    
    }

    public void CallColect()
    {
        m_isInZone=false;
        Debug.Log("E is pressed");
        m_InteractionText.text = "";
        m_interactor.OnPressCollect(inZoneObject);
    }
    IEnumerator OnGameFinish()
    {
        Cursor.lockState = CursorLockMode.None;
        m_InteractionText.text = "All materials are used!";
        m_gameFinishedPanel.gameObject.SetActive(true);
        m_audioManager.OnFinishGame();
        yield return new WaitForSeconds(3f);
        m_InteractionText.text = "";
        TopText.text = "";
        Time.timeScale = 0f;       
    }
    public void OnGameOver()
    {
        TopText.text = "";
        Time.timeScale = 0f;
        m_audioManager.OnGameOver();
        Cursor.lockState = CursorLockMode.None;
        m_gameOverPanel.SetActive(true);
    }

}
