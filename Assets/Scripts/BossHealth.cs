
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;

        Debug.Log("Boss HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            BossAttack attack = GetComponent<BossAttack>();

            if (attack != null)
                attack.enabled = false;

            foreach (BossProjectile projectile in
                     FindObjectsByType<BossProjectile>(
                         FindObjectsSortMode.None))
            {
                Destroy(projectile.gameObject);
            }

            WinScreen winScreen = FindFirstObjectByType<WinScreen>();

            if (winScreen != null)
            {
                winScreen.ShowWin();
                Debug.Log("YOU WIN panel shown!");
            }
            else
            {
                Debug.LogError("WinScreen not found in Level3!");
            }

            Destroy(gameObject);
        }
    }
}
