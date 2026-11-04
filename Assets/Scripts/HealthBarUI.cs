
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;

    Health playerHealth;
    BossHealth bossHealth;

    void Start()
    {
        playerHealth = GetComponentInParent<Health>();
        bossHealth = GetComponentInParent<BossHealth>();

        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
        }
    }

    void Update()
    {
        if (slider == null) return;

        if (bossHealth != null)
        {
            slider.value = Mathf.Clamp01(
                (float)bossHealth.currentHealth /
                bossHealth.maxHealth
            );
        }
        else if (playerHealth != null)
        {
            slider.value = Mathf.Clamp01(
                playerHealth.hp / 3f
            );
        }
    }
}
