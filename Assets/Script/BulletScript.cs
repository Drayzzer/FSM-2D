using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class BulletScript : MonoBehaviour
    {
        private PlayerLife _LifePlayer;
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
 
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
           { 
               Destroy(gameObject, _destroy);
           }
            
            if (other.gameObject.CompareTag("Player1"))
            {
                Destroy(gameObject, _destroy);
                _LifePlayer.TakeDamage();
            }
        }
    }
}
