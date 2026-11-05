using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Script.States
{
    public class GameBaseStateFire : BaseState
    {
        public GameBaseStateFire(GameObject host, float duration = -1) : base (host){}
        
        public override void OnEnter(GameplayHandler context)
        {
            _gameplayHandler = context;
            
            Vector2 right = _gameplayHandler.transform.right;
            Vector2 left = -_gameplayHandler.transform.right;

            Transform targetSpawn = _gameplayHandler._spriteRenderer.flipX ? _gameplayHandler._bulletSpawnLeft : _gameplayHandler._bulletSpawnRight;

            if (_gameplayHandler.FireTimer >= _gameplayHandler._fireCooldown)
            {
                _gameplayHandler._animator.SetTrigger("Attack");

                // Création d'une instance de "Bullet"
                BulletScript bullet = Object.Instantiate(_gameplayHandler._bulletPrefab, targetSpawn.position, Quaternion.identity).GetComponent<BulletScript>();

                // expression ternaire compresse le if en 1 ligne
                bullet.Fire(_gameplayHandler._spriteRenderer.flipX ? left : right);

                // j'applique une force a mon projectile qui fait reculer mon personnage
                _gameplayHandler._rigidbody2D.AddForce(Vector2.left * _gameplayHandler._fireRecoil);
                _gameplayHandler.FireTimer = 0;
            }
        }

        public override void OnUpdate(){}

        public override void OnExit(){}

        public override BaseState NextState() { throw new NotImplementedException(); }

        public void OnCollisionEnter(Collision other) {}
    }
}
