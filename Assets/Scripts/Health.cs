
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour
{
    public int hp = 3;
    public AudioClip hurtClip;
    public AudioClip healClip;

    SpriteRenderer sr;
    AudioSource audioSource;
    bool invincible;
    bool dead;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int dmg)
    {
        if (invincible || dead) return;

        hp -= dmg;

        if (audioSource != null && hurtClip != null)
            audioSource.PlayOneShot(hurtClip);

        if (hp <= 0)
        {
            dead = true;
            Invoke(nameof(GameOver), 0.5f);
            return;
        }

        StartCoroutine(Invincibility());
    }

    IEnumerator Invincibility()
    {
        invincible = true;

        for (int i = 0; i < 10; i++)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.2f);
        }

        sr.enabled = true;
        invincible = false;
    }

    public void Heal(int amount)
    {
        if (dead) return;

        hp += amount;

        if (audioSource != null && healClip != null)
            audioSource.PlayOneShot(healClip);
    }

    void GameOver()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}
