using UnityEngine;

namespace Script.States
{
    public class GameBaseStateJump : BaseState
    {
        public GameBaseStateJump(GameObject host, float duration = -1) : base(host) {}
        
        public override void OnEnter(GameplayHandler context)
        {
            _gameplayHandler = context;
            DoJump();
        }

        public override void OnUpdate() {}

        public override void OnExit() {}

        public void OnCollisionEnter(Collision other) {}

        public override BaseState NextState()
        {
            throw new System.NotImplementedException();
        }
        
        private void DoJump()
        {
            _gameplayHandler._rigidbody2D.AddForce(_gameplayHandler.transform.up * _gameplayHandler._jumpForce);
        }
    }
}
