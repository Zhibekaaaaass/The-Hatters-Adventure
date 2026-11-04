using UnityEngine;

public class Heart : Hazard
{
    public int healAmount = 1;

    public override void HitPlayer(Health playerHealth)
    {
        playerHealth.Heal(healAmount);
        Debug.Log("HP restored: " + playerHealth.hp);
    }
}