using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float speed = 2f;         // Kecepatan jalan
    public float range = 3f;        // Jarak patroli
    private float startingX;        // Titik awal
    private int direction = 1;      // Arah (1 kanan, -1 kiri)

    void Start()
    {
        // Simpan posisi awal pas game baru mulai
        startingX = transform.position.x;
    }

    void Update()
    {
        // Gerakan patroli kanan-kiri
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        // C.ek kalau sudah melewati batas jarak (range)
        if (Mathf.Abs(transform.position.x - startingX) >= range)
        {
            Flip();
        }
    }

    void Flip()
    {
        direction *= -1; // Balik arah logic

        // Balik arah gambar/sprite (biar nggak jalan mundur)
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    // Fungsi deteksi tabrakan (saat Vanellope nabrak badan musuh)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ambil script PlayerHealth dari Vanellope dan panggil fungsi TakeDamage
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage();
            }
        }
    }
}