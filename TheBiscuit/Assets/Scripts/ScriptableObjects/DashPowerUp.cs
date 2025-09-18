using UnityEngine;

[CreateAssetMenu(fileName = "DashPowerUp", menuName = "PowerUps/DashPowerUp")]
public class DashPowerUp : PowerUpData
{
    [Header("Dash PowerUp")]
    public float dashPower = 3f;
    public int dashCount = 5;

    public override void Apply(GameObject Target)
    {
        var switcher = Target.GetComponent<PowerUpManager>();
        switcher.isUsingDashPowerUp = true;
        switcher.isUsingSpeedPowerUp=false;
        switcher.dashPower = dashPower;
        switcher.dashCount = dashCount;
    }

}
