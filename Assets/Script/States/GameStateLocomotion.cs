using UnityEngine;

namespace Script.States
{
    public class GameStateLocomotion: GameStateBase
    {
        public override void EnterState(PlayerStateManager context)
        {
            _player = context;
        }

        public override void UpdateState()
        {
            DoLocomotion();

            if (_player.IsJumping)
                _player.SwitchState(_player.States.Jump);

            if (_player.IsCrouching)
                _player.SwitchState(_player.States.Crouch);

            if (_player.IsDodging)
                _player.SwitchState(_player.States.Dodge);

            if (_player.IsFiring)
                _player.SwitchState(_player.States.Fire);

            if (_player.IsGuarding)
                _player.SwitchState(_player.States.Guard);
        }

        public override void ExitState()
        {
        }

        public override void OnCollisionEnter(Collision other) {}

        // ----- //

        private void DoLocomotion()
        {
            float xMove = _player.Move.x * Time.deltaTime * _player.MoveSpeed;
            _player.Rigidbody.linearVelocity = new Vector2(xMove, _player.Rigidbody.linearVelocity.y);
            _player.transform.localScale = new Vector3(1, 1, 1);

            _player.CrouchingCollider.enabled = false;
            _player.StandingCollider.enabled = true;

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
        }
    }
}
