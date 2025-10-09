using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int MaxHealth = 2;
    public float currentHealth;
    public Animator _animator;
    public void Awake()
    {
        _animator = GetComponent<Animator>();
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("ca fonctionne");
        if (currentHealth >= 0)
        {
            _animator.SetTrigger("IsDamaged" );
            Debug.Log("se prends pas des dégâts");
        }
        if (currentHealth <= 0)
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
