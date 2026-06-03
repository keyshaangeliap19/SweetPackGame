using UnityEngine;
using UnityEngine.SceneManagement;

public class Candy : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Object.FindFirstObjectByType<ScoreManager>().AddScore(points);

            Destroy(gameObject);
        }
    }
}
