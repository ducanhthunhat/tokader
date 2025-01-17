using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public Animator anim;
    public float damage = 25f; // Số lượng sát thương của viên đạn
    private string currentName;
   

    void Start()
    {
        rb.velocity = transform.right * -speed;
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            rb.velocity = Vector2.zero;
            changeAnim("break");

            // Gây sát thương cho enemy
            enemyHealth Enemy = collision.GetComponent<enemyHealth>();
            if (Enemy != null)
            {
                Enemy.TakeDamage(damage);
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
