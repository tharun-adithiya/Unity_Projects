using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class GameOverScene : MonoBehaviour
{
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
    }
}
