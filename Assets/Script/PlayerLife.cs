using UnityEngine;

namespace Script
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 2;
        [SerializeField] private Animator _animator;

        private float _currentHealth;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            Debug.Log("ca fonctionne");
            if (_currentHealth >= 0)
            {
                _animator.SetTrigger("IsDamaged" );
                Debug.Log("se prends pas des dégâts");
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
