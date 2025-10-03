using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float Health = 2;
    public float MaxHealth = 2;
    public bool IsDead => Health <= 0;
    public float FireDamage = 1;

    void Start()
    {
    }

    public void TakeDamage()
    {
        Health -= FireDamage;
        
    }
}
