using UnityEngine;

[CreateAssetMenu(fileName = "SpeedPowerUp", menuName = "PowerUps/SpeedPowerUp")]
public class SpeedPowerUp : PowerUpData
{
    [Header("Speed PowerUp")]
    public float speedPower = 5f;
    public float powerUpDuration = 4f;
    public override void Apply(GameObject Target)
    {
        var manager = Target.GetComponent<PowerUpManager>();
        manager.isUsingSpeedPowerUp = true;
        manager.isUsingDashPowerUp = false;
        manager.speedUpPower=speedPower;
        manager.speedUpDuration = powerUpDuration;
    }
}
