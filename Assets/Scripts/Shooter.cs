
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public AudioClip shootClip;
    AudioSource audioSource;

    public GameObject bulletPrefab;
    public float cooldown = 0.3f;
    float nextShot;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Стрельба с клавиатуры X
        if (Keyboard.current != null &&
            Keyboard.current.xKey.isPressed &&
            Time.time > nextShot)
        {
            Shoot();
        }
    }

    // Этот метод вызывается и клавиатурой, и кнопкой FIRE
    public void Shoot()
    {
        if (Time.time < nextShot)
            return;

        nextShot = Time.time + cooldown;

        Quaternion rotation = sr != null && sr.flipX
            ? Quaternion.Euler(0, 0, 180)
            : Quaternion.identity;

        Instantiate(bulletPrefab, transform.position, rotation);

        if (audioSource != null && shootClip != null)
        {
            audioSource.PlayOneShot(shootClip);
        }
    }
}
