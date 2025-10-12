using UnityEngine;

namespace Script.States
{
    public class GameStateGuard : GameStateBase
    {
        public override void EnterState(PlayerStateManager context)
        {
            _player = context;
        }

        public override void UpdateState()
        {
            if (_player.IsGuarding)
                _player.SwitchState(_player.States.Locomotion);
        }

        public override void ExitState()
        {
        }

        public override void OnCollisionEnter(Collision other)
        {
        }
    }
}
