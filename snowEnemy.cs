using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private string currentName;
    public Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.velocity = Vector2.right * -speed; // Di chuyển đạn về phía trước
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            changeAnim("break");

            // Gây sát thương cho enemy
            HealthController player = collision.GetComponent<HealthController>();
            if (player != null)
            {
                player.TakeDamage(30);
            }

            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            changeAnim("break");
            StartCoroutine(DestroyAfterAnimation());
        }

    }
    private void changeAnim(string animName)
    {
        if (currentName != animName)
        {
            anim.ResetTrigger(animName);
            currentName = animName;
            anim.SetTrigger(currentName);
        }
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
