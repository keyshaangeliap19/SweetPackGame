using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public Button level2Button;

    void Start()
    {
        // Cek data di PlayerPrefs, kalau belum sampai level 2, tombol dimatikan
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        if (levelReached < 2)
        {
            level2Button.interactable = false; // Tombol jadi abu-abu/gak bisa diklik
        }
        else
        {
            level2Button.interactable = true;
        }
    }

    // Tips: Tambahkan fungsi buat reset data kalau mau testing
    public void ResetProgress() { PlayerPrefs.DeleteAll(); }
}