using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
