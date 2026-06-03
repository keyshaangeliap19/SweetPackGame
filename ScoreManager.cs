using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Menggunakan static agar bisa diakses dari script lain (misal script item)
    public static int score;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Reset skor ke 0 saat game dimulai
        score = 0;
        // Langsung tampilkan angka 0 saja
        UpdateScoreDisplay();
    }

    public void AddScore(int amount)
    {
        // Tambahkan jumlah skor
        score += amount;

        // Update tampilan teks
        UpdateScoreDisplay();
    }

    // Kita buat fungsi bantuan biar kodenya rapi dan tidak ngetik ulang
    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            // Hanya menampilkan angka skor saja
            scoreText.text = score.ToString();
        }
    }
}