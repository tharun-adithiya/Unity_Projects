using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenUIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_optionsPanel;
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void OnClickOptions()
    {
        
    }
}
