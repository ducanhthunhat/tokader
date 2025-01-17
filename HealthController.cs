using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public float _maxHealth = 100;
    private float _currentHealth;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private Player _player;
    [SerializeField] private float _damageAmount;
    private Coroutine _damageCoroutine; // Lưu Coroutine đang chạy
    private bool isPlayerInTrap = false;
    private string currentName;
    public Animator anim;
    AudioManager audioManager;
    [SerializeField] private Renderer _playerRenderer;
    private void Awake()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Start()
    {
        anim = GetComponent<Animator>();

    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealthBar();
        changeAnim("hit");

        audioManager.PlaySFX(audioManager.takedamage);

        if (_currentHealth <= 0)
        {
            _player.Die();
            changeAnim("die");
            audioManager.PlaySFX(audioManager.die);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            isPlayerInTrap = true;


            // Bắt đầu Coroutine trừ máu nếu chưa chạy
            if (_damageCoroutine == null)
            {
                _damageCoroutine = StartCoroutine(DamagePlayer());
            }
        }
        if (collision.CompareTag("Enemy"))
        {
            isPlayerInTrap = true;

            // Bắt đầu Coroutine trừ máu nếu chưa chạy
            if (_damageCoroutine == null)
            {
                _damageCoroutine = StartCoroutine(DamagePlayer());
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap") || collision.CompareTag("Enemy"))
        {
            isPlayerInTrap = false;
            // Dừng Coroutine khi người chơi rời khỏi bẫy
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
        }
    }

    private IEnumerator DamagePlayer()
    {
        while (isPlayerInTrap)
        {
            changeAnim("idle");
            TakeDamage(_damageAmount); // Trừ máu mỗi giây
            yield return new WaitForSeconds(1f);
            audioManager.PlaySFX(audioManager.takedamage);

        }
    }

    private void UpdateHealthBar()
    {
        _healthBarFill.fillAmount = _currentHealth / _maxHealth;
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
    
}
