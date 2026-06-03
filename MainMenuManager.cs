using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Tombol Level")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    void Start()
    {
        // PlayerPrefs akan menyimpan angka level tertinggi yang berhasil dibuka.
        // Di awal game, kalau belum pernah main, otomatis level yang terbuka adalah Level 1.
        int levelTerbuka = PlayerPrefs.GetInt("LevelTerbuka", 1);

        // --- LOGIKA MENGUNCI TOMBOL ---
        // Level 1 selalu bisa dipencet (interactable = true)
        level1Button.interactable = true;

        // Level 2 terbuka jika 'levelTerbuka' bernilai 2 atau lebih
        if (levelTerbuka >= 2)
        {
            level2Button.interactable = true;
            level2Button.image.color = Color.white; // Warna normal terang
        }
        else
        {
            level2Button.interactable = false; // Tombol mati tidak bisa diklik
            level2Button.image.color = new Color(0.5f, 0.5f, 0.5f, 0.7f); // Bikin warna agak abu-abu gelap (efek terkunci)
        }

        // Level 3 terbuka jika 'levelTerbuka' bernilai 3 atau lebih
        if (levelTerbuka >= 3)
        {
            level3Button.interactable = true;
            level3Button.image.color = Color.white;
        }
        else
        {
            level3Button.interactable = false;
            level3Button.image.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
        }
    }

    // Fungsi pembantu untuk pindah level saat tombolnya diklik
    public void BukaLevel(string namaScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(namaScene);
    }

    // --- TOMBOL RAHASIA UNTUK RESET DATA (OPSIONAL) ---
    // Bisa kamu panggil lewat tombol tersembunyi kalau mau ngetes ngunci semua level lagi dari awal
    public void ResetKunciLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload menu
    }
}