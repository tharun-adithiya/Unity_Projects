using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpUIManager : MonoBehaviour
{
    [SerializeField] private PowerUpSwitcher m_powerUpSwitcher;
    [SerializeField] private DashPowerUp m_dashPowerUpData;
    [SerializeField] private SpeedPowerUp m_speedPowerUpData;
    [SerializeField] GameObject powerUpPanel;
    [SerializeField] private Animator m_panelAnimator;
    [SerializeField] private AnimationClip m_panelOpenAnimation;
    [SerializeField] private AnimationClip m_panelCloseAnimation;
    [SerializeField] private Button m_button;
    public void OnChooseDashPowerUp()
    {
        m_powerUpSwitcher.SelectPowerUp(m_dashPowerUpData);
      
        StartCoroutine(ClosePowerUpPanel());
    }
    public void OnChooseSpeedPowerUp()
    {
        m_powerUpSwitcher.SelectPowerUp(m_speedPowerUpData);
        
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
