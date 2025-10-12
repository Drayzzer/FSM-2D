using UnityEngine;

namespace Script.States
{
    public class GameStateDodge : GameStateBase
    {
        public override void EnterState(PlayerStateManager context)
        {
            _player = context;

            float dodgePower = _player.Move.x * Time.deltaTime * _player.MoveSpeed * _player.DodgeMultiplier;
            _player.Rigidbody.linearVelocity = new Vector2(dodgePower, _player.Rigidbody.linearVelocity.y);

            _player.SwitchState(_player.States.Locomotion);
        }

        public override void UpdateState() {}

        public override void ExitState() {}

        public override void OnCollisionEnter(Collision other) {}
    }
}
