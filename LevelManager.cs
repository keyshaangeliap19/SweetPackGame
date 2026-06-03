using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // --- TAMBAHAN WAJIB UTK SLIDER ---
using UnityEngine.Audio; // --- TAMBAHAN WAJIB UTK MIXER ---

public class LevelManager : MonoBehaviour
{
    [Header("Panels Setup")]
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject settingsPanel;

    [Header("UI Texts Setup")]
    public TextMeshProUGUI scoreTextDiWinPanel;
    public TextMeshProUGUI scoreTextDiLosePanel;
    public TextMeshProUGUI soundButtonText;

    [Header("Win Celebration Setup")]
    public ParticleSystem confettiParticles;
    public AudioSource audioSource;
    public AudioClip winSound;

    // --- TAMBAHAN BARU UNTUK SLIDER VOLUME ---
    [Header("Audio Volume Setup")]
    public AudioMixer masterMixer; // Tempat naruh file MasterMixer kamu
    public Slider volumeSlider;    // Tempat naruh komponen VolumeSlider kamu

    private bool isSoundOn = true;

    // Fungsi otomatis berjalan saat game dimulai
    private void Start()
    {
        // Hubungkan slider agar saat digeser otomatis memanggil fungsi SetVolume di bawah
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);

            // Set posisi slider awal sesuai volume mixer saat ini
            float currentVol;
            if (masterMixer.GetFloat("MasterVol", out currentVol))
            {
                volumeSlider.value = currentVol;
            }
        }
    }

    // Fungsi gaib untuk mengubah volume berdasarkan geseran slider
    public void SetVolume(float value)
    {
        if (masterMixer != null)
        {
            // Jika slider digeser ke paling kiri (Min Value -40), kita buat benar-benar bisu (-80 dB)
            if (value <= -39f)
            {
                masterMixer.SetFloat("MasterVol", -80f);
            }
            else
            {
                masterMixer.SetFloat("MasterVol", value);
            }
        }
    }

    // --- FUNGSI PANEL MENANG / KALAH ---
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        if (scoreTextDiWinPanel != null)
        {
            scoreTextDiWinPanel.text = ScoreManager.score.ToString();
        }

        if (confettiParticles != null)
        {
            confettiParticles.gameObject.SetActive(true);
            var emission = confettiParticles.emission;
            emission.rateOverTime = 100f;
            confettiParticles.Play();
        }

        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        if (scoreTextDiLosePanel != null)
        {
            scoreTextDiLosePanel.text = ScoreManager.score.ToString();
        }
        Time.timeScale = 0f;
    }

    // --- FUNGSI PANEL SETTINGS ---
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        if (soundButtonText != null)
        {
            if (isSoundOn)
            {
                soundButtonText.text = "SOUND: ON";
                masterMixer.SetFloat("MasterVol", volumeSlider.value); // Kembalikan ke volume slider
                Debug.Log("Suara Game: ON");
            }
            else
            {
                soundButtonText.text = "SOUND: OFF";
                masterMixer.SetFloat("MasterVol", -80f); // Mute total lewat mixer
                Debug.Log("Suara Game: OFF");
            }
        }
    }

    // --- FUNGSI TOMBOL AKSI ---
    public void RetryLevel() { Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void NextLevel() { Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    public void BackToMenu() { Time.timeScale = 1f; SceneManager.LoadScene("SampleScene"); }
}