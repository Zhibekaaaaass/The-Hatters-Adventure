using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damage = 1;

    public virtual void HitPlayer(Health playerHealth)
    {
        playerHealth.TakeDamage(damage);
    }
}