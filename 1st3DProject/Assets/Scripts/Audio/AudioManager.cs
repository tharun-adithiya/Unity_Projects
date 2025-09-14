using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public enum AudioType { GameOver, GameFinishedEffect, GameFinishedMusic}
    public PlayerMovement playerMovement;
    [Header("Background Audio")]
    public List<AudioClip> bgAudioList = new List<AudioClip>();
    [SerializeField] private AudioSource m_MainAudioSource;
    public Dictionary<AudioType, AudioClip> m_audioDictionary = new Dictionary<AudioType, AudioClip> ();

    [Header("AudioSources for interaction")]
    
    [SerializeField] private AudioSource m_playerAudioSource;
    public AudioSource interactionAudioSource;

    [Header("Audio to ShutDown")]
    [SerializeField] private List<AudioSource> m_activeAudioSources= new List<AudioSource> ();

    [Header("GameOver/Gamefinish Audio")]
    [SerializeField] private AudioClip m_gameOverAudio;
    [SerializeField] private AudioClip m_gameCompletedAudio;
    [SerializeField] private AudioClip m_gameCompletedMusic;
    private int m_audioCount = 0;

    [SerializeField] private AudioClip m_playerHurtAudio;

    private void Start()
    {
        m_activeAudioSources[2].volume = 0.75f;
        m_audioDictionary.Add(AudioType.GameOver, m_gameOverAudio);
        m_audioDictionary.Add(AudioType.GameFinishedEffect, m_gameCompletedAudio);
    }
    void Update()
    {

        if (!m_MainAudioSource.isPlaying)
        {
            Debug.Log("SwitchingAudio");
            m_audioCount = Convert.ToInt32(UnityEngine.Random.Range(0, bgAudioList.Count));
            m_MainAudioSource.clip = bgAudioList[m_audioCount];
            m_MainAudioSource.Play();
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && playerMovement.IsGroundedCheckForAudio())
        {
            Debug.Log("AudioSource Enabled");
            m_playerAudioSource.enabled = true;
        }
        else
        {
            Debug.Log("AudioSource Disabled");
            m_playerAudioSource.enabled = false;
        }
    }

    public void StopAudioSources()
    {
        interactionAudioSource.Stop();
        foreach (var source in m_activeAudioSources)
        {
            source.volume = 0;
            Debug.Log($"Stoping audio source:{source}");
        }
    }

    public void OnPlayerDamage()
    {
        Debug.Log("Player gets damage");
        interactionAudioSource.clip = m_playerHurtAudio;
        interactionAudioSource.pitch = UnityEngine.Random.Range(1,1.5f);
        interactionAudioSource.Play();
    }

    public void OnGameOver()
    {
        Debug.Log("Playing Game Over audio");
        m_MainAudioSource.clip = m_audioDictionary[AudioType.GameOver];
        StopAudioSources();
        m_MainAudioSource.Play();
    }
    public void OnFinishGame()
    {
        Debug.Log("Playing Game finished audio");
        m_MainAudioSource.clip = m_audioDictionary[AudioType.GameFinishedEffect];
        StopAudioSources();
        m_MainAudioSource.Play();
        
    }
}
