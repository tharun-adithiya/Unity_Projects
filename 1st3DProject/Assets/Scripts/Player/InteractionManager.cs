using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManger : MonoBehaviour
{
    private PlayerAudio m_playerAudio;
    [SerializeField] private PlayerInventory m_playerInventory;
    [SerializeField] private PlayerMovement m_playerMovement;
    public List<AudioClip> playerInteractionAudio = new List<AudioClip>();
    private void Start()
    {
        m_playerAudio = GameObject.FindGameObjectWithTag("PlayerStructure").GetComponent<PlayerAudio>();
        if (m_playerAudio == null)
        {
            Debug.LogWarning("Player audio reference is not found");
        }
        Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
    }
    public void VoiceLineSwitcher(string GameObjName)
    {
        switch (GameObjName) 
        {
            case "BubbleSphereRTX":
                Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
                Debug.Log(GameObjName+"Found");
                m_playerAudio.playerVoice.clip = playerInteractionAudio[0];
                m_playerAudio.playerVoice.Play();
                break;
            case "BubbleSpherePanel":
                Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
                Debug.Log(GameObjName + "Found");
                m_playerAudio.playerVoice.clip = playerInteractionAudio[1];
                m_playerAudio.playerVoice.Play();
                break;
            case "BubbleSphereBattery":
                Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
                Debug.Log(GameObjName + "Found");
                m_playerAudio.playerVoice.clip = playerInteractionAudio[2];
                m_playerAudio.playerVoice.Play();
                break;
            case "BubbleSphereAru":
                Debug.Log($"{playerInteractionAudio.Count}: AudioListSize");
                Debug.Log(GameObjName + "Found");
                m_playerAudio.playerVoice.clip = playerInteractionAudio[3];
                m_playerAudio.playerVoice.Play();
                break;
        }
        if (m_playerInventory.Inventory.Count >= 5)
        {
            StartCoroutine(PlayFinalAudio());
        }
    }
    IEnumerator PlayFinalAudio()
    {
        
        m_playerAudio.playerVoice.clip = playerInteractionAudio[4];
        yield return new WaitForSeconds(5f);
        m_playerAudio.playerVoice.Play();
    }
}

