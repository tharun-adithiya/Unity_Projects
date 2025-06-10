using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenUIManager : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }
}
