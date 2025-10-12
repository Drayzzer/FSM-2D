using UnityEngine;

namespace Script.States
{
    public class GameStateCrouch: GameStateBase
    {
        public override void EnterState(PlayerStateManager context)
        {
            _player = context;

            float xMove = _player.Move.x * Time.deltaTime * _player.MoveSpeed * _player.CrouchMultiplier;
            _player.Rigidbody.linearVelocity = new Vector2(xMove, _player.Rigidbody.linearVelocity.y);

            if (!_player.CrouchingCollider.enabled)
            {
                _player.CrouchingCollider.enabled = true;
                _player.StandingCollider.enabled = false;
            }

            _player.SwitchState(_player.States.Locomotion);
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            _player.CrouchingCollider.enabled = false;
            _player.StandingCollider.enabled = true;
        }

        public override void OnCollisionEnter(Collision other)
        {
        }
    }
}
