using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearts;
    public Animator animator;

    private float invincibilityTimer;
    public float invincibilityDuration = 1.5f;

    [Header("Audio Setup")]
    // Kotak untuk menaruh komponen Audio Source yang ada di tubuh Vanellope
    public AudioSource audioSource;
    // Kotak untuk menaruh file audio .mp3 atau .wav kesakitan/hantaman
    public AudioClip hurtSound;

    void Update()
    {
        // Logika waktu kebal agar tidak langsung mati berkali-kali dalam sekejap
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek jika menabrak Lumpur atau Musuh saat tidak sedang kebal
        if ((other.CompareTag("Mud") || other.CompareTag("Enemy")) && invincibilityTimer <= 0)
        {
            TakeDamage();
            invincibilityTimer = invincibilityDuration;
        }
    }

    public void TakeDamage()
    {
        if (health <= 0) return;

        health--;

        // Mematikan gambar hati di UI
        if (health >= 0 && health < hearts.Length)
        {
            hearts[health].SetActive(false);
        }

        // Jalankan animasi kena pukul/sakit
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        // --- SEGMEN BARU: MEMUTAR EFEK SUARA ---
        // Dicek dulu agar tidak menyebabkan error "NullReferenceException" jika lupa ditarik
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound); // Putar suara sekali tanpa memutus BGM
        }
        // ----------------------------------------

        // Jika darah habis, panggil Game Over
        if (health <= 0)
        {
            Debug.Log("GAME OVER: Vanellope Kehabisan Darah!");

            // MENCARI LEVEL MANAGER DAN MUNCULKAN PANEL
            LevelManager manager = Object.FindFirstObjectByType<LevelManager>();

            if (manager != null)
            {
                manager.ShowGameOverPanel();
            }
            else
            {
                // Pesan ini muncul di Console kalau kamu lupa naruh LevelManager_Object di Hierarchy
                Debug.LogError("Error: LevelManager tidak ditemukan di Scene ini! Cek Hierarchy kamu.");
            }
        }
    }
}