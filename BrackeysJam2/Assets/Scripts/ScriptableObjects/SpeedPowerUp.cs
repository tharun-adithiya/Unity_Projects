using UnityEngine;

[CreateAssetMenu(fileName = "SpeedPowerUp", menuName = "PowerUps/SpeedPowerUp")]
public class SpeedPowerUp : PowerUpData
{
    [Header("Speed PowerUp")]
    public float speedPower = 5f;
    public float powerUpDuration = 4f;
    public override void Apply(GameObject Target)
    {
        var switcher = Target.GetComponent<PowerUpSwitcher>();
        switcher.isUsingSpeedPowerUp = true;
        switcher.isUsingDashPowerUp = false;
        switcher.speedUpPower=speedPower;
        switcher.speedUpDuration = powerUpDuration;
    }
}
