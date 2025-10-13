using UnityEngine;

namespace Script
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 5;
        [SerializeField] private Animator _animator;
        
        public HealthBar healthBar;

        private int _currentHealth;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;
            healthBar.SetMaxHealth(_maxHealth);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            
            healthBar.SetHealth(_currentHealth);
            if (_currentHealth >= 0)
            {
                _animator.SetTrigger("IsDamaged" );
            }
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
        public void Die()
        {
            _animator.SetBool("Dead", true );
            Debug.Log("ce prend des dégâts");
        }
    }
}