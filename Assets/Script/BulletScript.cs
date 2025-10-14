using UnityEngine;

namespace Script
{
    public class BulletScript : MonoBehaviour
    {
        public Rigidbody2D _rb;
        public float BulletForce;
        public GameObject Gun;
        [SerializeField] private float _destroy;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            BulletForce = 600;
            _rb.AddForce(Vector2.right * BulletForce);
            transform.position = Gun.transform.position;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
           { 
               Destroy(gameObject, _destroy);
           }
            
            if (other.gameObject.CompareTag("Player1"))
            {
                PlayerLife playerLife = other.gameObject.GetComponent<PlayerLife>();
                Debug.Log("playerlife marche");
                playerLife.TakeDamage(1f);
                
                Destroy(gameObject, _destroy);
            }
        }
    }
}
