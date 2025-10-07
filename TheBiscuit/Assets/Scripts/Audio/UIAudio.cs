using UnityEngine;
using System;
using System.ComponentModel;

public class UIAudio : MonoBehaviour
{
    public event Action OnPlayUIButtonSound;

    public void TriggerOnPlayUIButtonSound()
    {
        OnPlayUIButtonSound?.Invoke();
    }
    
}
