using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenUIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_controlsPanel;
    private void Update()
    {
        if (m_controlsPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickBack();
        }
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickQuit()
    {
       // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void OnClickOptions()
    {
        m_controlsPanel.SetActive(true);
    }
    public void OnClickBack()
    {
        m_controlsPanel.SetActive(false);
    }

}
