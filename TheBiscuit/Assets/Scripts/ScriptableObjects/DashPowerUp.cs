using UnityEngine;

[CreateAssetMenu(fileName = "DashPowerUp", menuName = "PowerUps/DashPowerUp")]
public class DashPowerUp : PowerUpData
{
    [Header("Dash PowerUp")]
    public float dashPower = 3f;
    public int dashCount = 5;

    public override void Apply(GameObject Target)
    {
        var manager = Target.GetComponent<PowerUpManager>();
        manager.isUsingDashPowerUp = true;
        manager.isUsingSpeedPowerUp=false;
        manager.dashPower = dashPower;
        manager.dashCount = dashCount;
    }

}
