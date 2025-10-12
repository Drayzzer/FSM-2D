using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Script.States
{
    public class GameStateFire : GameStateBase
    {
        public override void EnterState(PlayerStateManager context)
        {
            _player = context;

            Vector2 right = _player.transform.right;
            Vector2 left = -_player.transform.right;

            Transform targetSpawn = _player.IsFlipped ? _player.BulletSpawnLeft : _player.BulletSpawnRight;

            if (_player.FireTimer >= _player.FireCooldown)
            {
                _player.Animator.SetTrigger("Attack");

                // Création d'une instance de "Bullet"
                BulletScript bullet = Object.Instantiate(_player.BulletPrefab, targetSpawn.position, Quaternion.identity).GetComponent<BulletScript>();

                // expression ternaire compresse le if en 1 ligne
                bullet.Fire(_player.IsFlipped ? left : right);

                // j'applique une force a mon projectile qui fait reculer mon personnage
                _player.Rigidbody.AddForce(Vector2.left * _player.FireRecoil);
                _player.FireTimer = 0;
            }

            _player.SwitchState(_player.States.Locomotion);
        }

        public override void UpdateState()
        {

        }

        public override void ExitState() {}

        public override void OnCollisionEnter(Collision other) {}
    }
}
