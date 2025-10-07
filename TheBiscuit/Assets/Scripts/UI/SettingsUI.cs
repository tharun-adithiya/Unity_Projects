using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{   
    public event Action OnClickClose;
    public void TriggerOnClickClose()
    {
        OnClickClose?.Invoke();
    }
}
