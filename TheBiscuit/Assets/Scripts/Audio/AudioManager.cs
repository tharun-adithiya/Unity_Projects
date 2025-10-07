using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private UIAudio m_uiAudio;
    [SerializeField] private AudioSource m_uiAudioSource;
    [SerializeField] private AudioClip m_uiClickSound;

    public AudioManager Instance { get; private set; }
    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    private void OnEnable()
    {
        m_uiAudio.OnPlayUIButtonSound += PlayButtonClickAudio;                  //Subscribing to UIAudio's OnPlayUIButtonSoundEvent
    }
    private void OnDisable()
    {
        m_uiAudio.OnPlayUIButtonSound -= PlayButtonClickAudio;                 //Unsubscribing to UIAudio's OnPlayUIButtonSoundEvent
    }
    public void PlayButtonClickAudio()
    {
        m_uiAudioSource.clip = m_uiClickSound;
        Debug.Log("Audio is playing");
        if (!m_uiAudioSource.isPlaying)                     //To prevent audio overlapping
        {
            m_uiAudioSource.Play();
        }
    }
}
