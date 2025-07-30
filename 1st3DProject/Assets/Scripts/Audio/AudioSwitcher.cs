using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    [SerializeField]private AudioManager m_audioManager;
    [SerializeField] private AudioSource m_MainAudioSource;
    [SerializeField] private AudioClip m_gameOverAudio;
    [SerializeField] private AudioClip m_gameCompletedAudio;

    private void Start()
    {
        m_MainAudioSource.clip=m_audioManager.audioList[0];
        m_audioManager.m_audioDictionary.Add("GameOverAudio", m_gameOverAudio);
        m_audioManager.m_audioDictionary.Add("GameCompletedAudio", m_gameCompletedAudio);
    }
    void Update()
    {
        if (!m_MainAudioSource.isPlaying)
        {
            Debug.Log("SwitchingAudio");
            m_MainAudioSource.clip = m_audioManager.audioList[1];
        }
    }

    public void OnGameOver()
    {
        m_MainAudioSource.clip = m_audioManager.m_audioDictionary["GameOverAudio"];
    }
    public void OnFinishGame()
    {
        m_MainAudioSource.clip = m_audioManager.m_audioDictionary["GameCompletedAudio"];
    }
}
