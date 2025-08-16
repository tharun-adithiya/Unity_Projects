using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManger : MonoBehaviour
{
    private AudioManager m_audioManager;
    private bool m_isFinalAudioPlayed=false;
    [SerializeField] private PlayerInventory m_playerInventory;
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private GameManager m_gameManager;
    public List<AudioClip> playerInteractionAudio = new List<AudioClip>();
    private void Start()
    {
        m_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (m_audioManager == null)
        {
            Debug.LogWarning("Player audio reference is not found");
        }
        Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
    }
    private void Update()
    {
        if (m_playerInventory.Inventory.Count >= 4&&!m_isFinalAudioPlayed)
        {
            Debug.Log("Final Audio is called");
            StartCoroutine(PlayFinalAudio());
            m_isFinalAudioPlayed = true;
        }
    }
    public void VoiceLineSwitcher(string GameObjName)
    {
        switch (GameObjName) 
        {
            case "BubbleSphereRTX":
                m_gameManager.OnCollectMaterial();
                Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
                Debug.Log(GameObjName+"Found");
                m_audioManager.interactionAudioSource.clip = playerInteractionAudio[0];
                m_audioManager.interactionAudioSource.Play();
                break;
            case "BubbleSpherePanel":
                m_gameManager.OnCollectMaterial();
                Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
                Debug.Log(GameObjName + "Found");
                m_audioManager.interactionAudioSource.clip = playerInteractionAudio[1];
                m_audioManager.interactionAudioSource.Play();
                break;
            case "BubbleSphereBattery":
                m_gameManager.OnCollectMaterial();
                Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
                Debug.Log(GameObjName + "Found");
                m_audioManager.interactionAudioSource.clip = playerInteractionAudio[2];
                m_audioManager.interactionAudioSource.Play();
                break;
            case "BubbleSphereAru":
                m_gameManager.OnCollectMaterial();
                Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
                Debug.Log(GameObjName + "Found");
                m_audioManager.interactionAudioSource.clip = playerInteractionAudio[3];
                m_audioManager.interactionAudioSource.Play();
                break;
        }

    }
    IEnumerator PlayFinalAudio()
    {     
        yield return new WaitForSeconds(5f);
        m_audioManager.interactionAudioSource.clip = playerInteractionAudio[4];
        m_audioManager.enabled = true;
        m_audioManager.interactionAudioSource.Play();
        
    }
}

