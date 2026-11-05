using System;
using UnityEngine;

namespace Script
{
    public class BulletScript : MonoBehaviour
    {
        public Rigidbody2D _rb;
        public float BulletForce;
        [SerializeField] private float _destroy;


        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Fire(Vector2 direction)
        {
            _rb.AddForce(direction * BulletForce);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject, _destroy);
            }
            else if (other.gameObject.CompareTag("Player1"))
            {
                GameplayHandler gameplayHandler = other.GetComponent<GameplayHandler>();
                PlayerLife playerLife = other.GetComponent<PlayerLife>();

                if (!gameplayHandler)
                    throw new Exception($"Bullet can't find a PlayerStateManager component on object: {other.gameObject.name}");
                if (!playerLife)
                    throw new Exception($"Bullet can't find a PlayerLife component on object: {other.gameObject.name}");


                if (gameplayHandler.IsGuarding.Value)
                    playerLife.TakeDamage(1);
                else
                    playerLife.TakeDamage(2);

                Destroy(gameObject, _destroy);
            }
        }
    }
}
