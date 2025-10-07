using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public event Action OnClickStartButton;
    public event Action OnClickSettingsButton;
    public event Action OnClickExitButton;
   
    public void TriggerStartButton()
    {
        Debug.Log("Start button is clicked");
        OnClickStartButton?.Invoke();
        if (OnClickStartButton == null)
        {
            Debug.Log("Method not assigned for starting");
        }
    }
    public void TriggerSettingsButton() 
    {
        Debug.Log("Settings button is clicked");
        OnClickSettingsButton?.Invoke();
        if (OnClickStartButton == null)
        {
            Debug.Log("Method not assigned for settings button");
        }
    }

    public void TriggerExitButton()
    {
        Debug.Log("Exit button is clicked");
        OnClickExitButton?.Invoke();
        if (OnClickExitButton == null)
        {
            Debug.Log("Method not assigned for exit button");
        }
    }
}
