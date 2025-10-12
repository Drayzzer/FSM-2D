using UnityEngine;

namespace Script.States
{
    public class GameStateDoAir : GameStateBase
    {
        public override void EnterState(PlayerStateManager context)
        {
            _player = context;
        }

        public override void UpdateState()
        {
            float xMove = _player.Move.x * Time.deltaTime * _player.MoveSpeed * _player.AirMultiplier;
            _player.Rigidbody.linearVelocity = new Vector2(xMove, _player.Rigidbody.linearVelocity.y);

            if (_player.Rigidbody.linearVelocity.x > 0)
            {
                _player.SpriteRenderer.flipX = false;
                _player.IsFlipped = false;
            }

            if (_player.Rigidbody.linearVelocity.x < 0)
            {
                _player.SpriteRenderer.flipX = true;
                _player.IsFlipped = true;
            }
            
            if (_player.IsFiring)
            {
                _player.SwitchState(_player.States.Fire);
            }
            else if (_player.IsDodging)
            {
                _player.SwitchState(_player.States.Dodge);
            }
            else if (_player.IsGrounded)
            {
                _player.SwitchState(_player.States.Locomotion);
            }
        }

        public override void ExitState()
        {
        }

        public override void OnCollisionEnter(Collision other)
        {
        }
    }
}
