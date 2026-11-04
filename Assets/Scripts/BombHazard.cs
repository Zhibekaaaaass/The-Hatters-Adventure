
using UnityEngine;

public class BombHazard : Hazard
{
    public override void HitPlayer(Health playerHealth)
    {
        playerHealth.TakeDamage(2);
    }
}
