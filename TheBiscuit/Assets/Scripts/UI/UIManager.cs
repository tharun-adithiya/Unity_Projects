using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuUI m_mainMenuUI;
    [SerializeField] private SettingsUI m_settingsUI;
    [SerializeField] private GameObject SettingsPanel;
    public static UIManager Instance { get; private set; }
    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    private void OnEnable()
    {
        m_mainMenuUI.OnClickSettingsButton += OpenSettingsPanel;
        m_settingsUI.OnClickClose += CloseSettingsPanel;
    }
    private void OnDisable()
    {
        m_mainMenuUI.OnClickSettingsButton -= OpenSettingsPanel; 
        m_settingsUI.OnClickClose -= CloseSettingsPanel;
    }

    private void OpenSettingsPanel()
    {
        if (SettingsPanel == null)
        {
            Debug.LogWarning("Settings panel is not assigned in UIManager");
        }
        SettingsPanel.SetActive(true);
    }

    private void CloseSettingsPanel()
    {
        if (SettingsPanel == null)
        {
            Debug.LogWarning("Settings panel is not assigned in UIManager");
        }
        SettingsPanel.SetActive(false);
    }
}
