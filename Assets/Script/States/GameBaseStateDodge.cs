using UnityEngine;

namespace Script.States
{
    public class GameBaseStateDodge : BaseState
    {
        public GameBaseStateDodge(GameObject host) : base(host) {}
        
        public override void OnEnter(GameplayHandler context)
        {
            _gameplayHandler = context;

            float dodgePower = _gameplayHandler.Move.x * Time.deltaTime * _gameplayHandler._moveSpeed * _gameplayHandler._dodgeMultiplier;
            _gameplayHandler._rigidbody2D.linearVelocity = new Vector2(dodgePower, _gameplayHandler._rigidbody2D.linearVelocity.y);
        }

        public override void OnUpdate()
        {
            DoDodge();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override BaseState NextState()
        {
            return new GameBaseStateLocomotion(_gameplayHandler.gameObject);
        }


        public void OnCollisionEnter(Collision other) {}
        
        private void DoDodge()
        {
            _gameplayHandler.Dodge = _gameplayHandler.Move.x * Time.deltaTime * _gameplayHandler._moveSpeed * _gameplayHandler._dodgeMultiplier;
            _gameplayHandler._rigidbody2D.linearVelocity = new Vector2(_gameplayHandler.Dodge, _gameplayHandler._rigidbody2D.linearVelocity.y);
        }
    }
}
