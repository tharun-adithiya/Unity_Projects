using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class GameOverScene : MonoBehaviour
{
    public AudioSwitcher m_audioSwitcher;

    public void GameOver()
    {
        Time.timeScale = 0f;
        m_audioSwitcher.OnGameOver();
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
    }
}
