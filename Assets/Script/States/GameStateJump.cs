using UnityEngine;

namespace Script.States
{
    public class GameStateJump : GameStateBase
    {
        public override void EnterState(PlayerStateManager context)
        {
            _player = context;

            _player.Rigidbody.AddForce(_player.transform.up * _player.JumpForce);

            _player.SwitchState(_player.States.DoAir);
        }

        public override void UpdateState() {}

        public override void ExitState() {}

        public override void OnCollisionEnter(Collision other) {}
    }
}
