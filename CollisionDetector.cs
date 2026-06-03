using UnityEngine;
using UnityEngine.SceneManagement; // WAJIB DITAMBAHKAN biar script bisa membaca nama level/scene kamu

public class CollisionDetector : MonoBehaviour
{
    // Tentukan target skor minimum untuk lolos level di sini (Bisa kamu ubah sesuai keinginan)
    public int targetScoreToWin = 100;

    // Jika Collider Goa TIDAK dicentang "Is Trigger" (Tabrakan Fisik)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            CekKondisiSkor();
        }
    }

    // Jika Collider Goa DICENTANG "Is Trigger" (Bisa ditembus)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            CekKondisiSkor();
        }
    }

    // --- LOGIKA UTAMA PENGECEKAN KONDISI SKOR & UNLOCK LEVEL ---
    void CekKondisiSkor()
    {
        LevelManager manager = Object.FindFirstObjectByType<LevelManager>();

        if (manager != null)
        {
            // Ambil skor asli saat ini dari ScoreManager kamu
            int currentScore = ScoreManager.score;

            // KONDISI 1: Jika skor pemain SUDAH CUKUP / mencapai target
            if (currentScore >= targetScoreToWin)
            {
                Debug.Log("Skor Cukup! Vanellope Lolos Level.");

                // --- SEGMEN BARU: LOGIKA MENYIMPAN KUNCI LEVEL ---
                // Mengambil nama Scene/Level yang sedang dimainkan saat ini secara otomatis
                string sceneAktif = SceneManager.GetActiveScene().name;

                // Ambil data level tertinggi yang pernah dibuka sebelumnya (default adalah 1 jika baru main)
                int dataLama = PlayerPrefs.GetInt("LevelTerbuka", 1);

                if (sceneAktif == "Level 1")
                {
                    // Jika menang di Level 1, buka kunci Level 2
                    // Mathf.Max digunakan agar jika player main ulang Level 1, data Level 3 yang sudah kebuka tidak tertutup lagi
                    PlayerPrefs.SetInt("LevelTerbuka", Mathf.Max(dataLama, 2));
                }
                else if (sceneAktif == "Level 2")
                {
                    // Jika menang di Level 2, buka kunci Level 3
                    PlayerPrefs.SetInt("LevelTerbuka", Mathf.Max(dataLama, 3));
                }

                // Simpan data ke dalam memori perangkat secara permanen
                PlayerPrefs.Save();
                // -------------------------------------------------

                manager.ShowWinPanel(); // Munculin Panel Menang
            }
            // KONDISI 2: Jika skor pemain MASIH KURANG dari target
            else
            {
                Debug.Log("Skor Kurang! Vanellope Gagal Memenuhi Target.");
                manager.ShowGameOverPanel(); // Munculin Panel Game Over / Kalah
            }
        }
        else
        {
            Debug.LogError("Error: LevelManager_Object tidak ditemukan di Scene!");
        }
    }
}