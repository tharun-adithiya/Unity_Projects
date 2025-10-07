using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpUIManager : MonoBehaviour
{
    [SerializeField] private PowerUpManager m_powerUpManager;
    [SerializeField] private DashPowerUp m_dashPowerUpData;
    [SerializeField] private SpeedPowerUp m_speedPowerUpData;
    [SerializeField] GameObject powerUpPanel;
    [SerializeField] private Animator m_panelAnimator;
    [SerializeField] private AnimationClip m_panelOpenAnimation;
    [SerializeField] private AnimationClip m_panelCloseAnimation;
    
    public void OnChooseDashPowerUp()
    {
        m_powerUpManager.SelectPowerUp(m_dashPowerUpData);
      
        StartCoroutine(ClosePowerUpPanel());
    }
    public void OnChooseSpeedPowerUp()
    {
        m_powerUpManager.SelectPowerUp(m_speedPowerUpData);
        
        StartCoroutine(ClosePowerUpPanel());
    }
    public IEnumerator OpenPowerUpPanel()
    {
        powerUpPanel.SetActive(true);
        m_panelAnimator.SetBool("IsOpen",true); 
        yield return null;
    }
    public IEnumerator ClosePowerUpPanel()
    {
        m_panelAnimator.SetBool("IsOpen", false);
        yield return new WaitForSeconds(m_panelCloseAnimation.length);
        powerUpPanel.SetActive(false);
    }
     
}
