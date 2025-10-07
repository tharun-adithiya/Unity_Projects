using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }
    [SerializeField] private MainMenuUI m_mainMenuUI;
    [SerializeField] private GameManager m_gameManager;

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
    private void OnEnable()                                 //Subscribing
    { 
       m_mainMenuUI.OnClickStartButton += StartGame;
        
       m_gameManager.OnGameOver += GameOver;

       m_mainMenuUI.OnClickExitButton += ExitGame;
    }

    private void OnDisable()                                //Unsubscribing
    {   
        m_mainMenuUI.OnClickStartButton -= StartGame;

        m_gameManager.OnGameOver -= GameOver;

        m_mainMenuUI.OnClickExitButton -= ExitGame;
    }


    public void StartGame()
    {
        WaitBeforeGameStart();
        SceneManager.LoadScene(1);
        Debug.Log("Game Started");
    }
    public void GameOver()
    {
        StartCoroutine(WaitBeforeGameOver());
    }
    public void ExitGame()
    {
        Debug.Log("Game Quits");
        Application.Quit();
    }
    IEnumerator WaitBeforeGameStart()
    {
        Debug.Log("Game Starts");
        yield return new WaitForSeconds(1f);
        
        //yield return null;
    }
    IEnumerator WaitBeforeGameOver()
    {
       // yield return new WaitForSeconds(0.6f);
        Debug.Log("Game Over");
        SceneManager.LoadScene(1);
       yield return null;
    }
}
