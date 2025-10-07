using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    [HideInInspector] public bool isUsingSpeedPowerUp=false;
    [HideInInspector] public bool isUsingDashPowerUp=false;
    [HideInInspector] public float speedUpPower;
    [HideInInspector] public float speedUpDuration;
    [HideInInspector] public int dashCount;
    [HideInInspector] public float dashPower;

    [SerializeField]private SpeedPowerUp m_speedPowerUpData;
    [SerializeField]private DashPowerUp m_dashPowerUpData;
    
    
    private PowerUpData m_currentPowerUp;


    public void SelectPowerUp(PowerUpData powerUp)
    {
        m_currentPowerUp = powerUp;
        m_currentPowerUp.Apply(gameObject);
    }

}