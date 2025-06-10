using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private Button m_restartButton;
    [SerializeField] private Button m_mainMenuButton;
    
    public void OnClickRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
