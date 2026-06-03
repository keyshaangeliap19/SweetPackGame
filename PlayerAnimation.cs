using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    public Rigidbody2D rb;
    public Animator animator;

    private bool isGrounded;

    // Variabel baru untuk logika balik badan (Flip)
    private bool isFacingRight = true;

    void Update()
    {
        // --- GERAK ---
        float move = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // --- LOGIKA FLIP (BALIK BADAN) ---
        // Jika gerak ke kanan (move > 0) tapi lagi hadap kiri, balik badan
        if (move > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        // Jika gerak ke kiri (move < 0) tapi lagi hadap kanan, balik badan
        else if (move < 0 && isFacingRight)
        {
            FlipCharacter();
        }

        // --- ANIMASI RUN ---
        animator.SetFloat("Speed", Mathf.Abs(move));

        // --- LOMPAT ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // --- ANIMASI JUMP / FALL ---
        if (rb.linearVelocity.y > 0.1f)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    // --- FUNGSI BARU UNTUK BALIK BADAN ---
    void FlipCharacter()
    {
        // Tukar status hadap
        isFacingRight = !isFacingRight;

        // Ambil skala karakter sekarang
        Vector3 currentScale = transform.localScale;

        // Kalikan skala X dengan -1 (ini yang bikin gambar kebalik)
        currentScale.x *= -1;

        // Pasang lagi skalanya ke karakter
        transform.localScale = currentScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}