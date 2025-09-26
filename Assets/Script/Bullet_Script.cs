using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public Rigidbody2D _rb;
    public float BulletForce;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        BulletForce = 230;
        _rb.AddForce(Vector2.right * BulletForce);
        
    }
}
