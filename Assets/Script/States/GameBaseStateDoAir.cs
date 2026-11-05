using UnityEngine;

namespace Script.States
{
    public class GameBaseStateDoAir : BaseState
    {
        public GameBaseStateDoAir(GameObject host) : base(host) {}
        
        public override void OnEnter(GameplayHandler context)
        {
            _gameplayHandler = context;
        }

        public override void OnUpdate()
        {
            DoAir(); 
        }

        public override void OnExit(){}

        public override BaseState NextState()
        {
            if (_gameplayHandler.IsDodging.Value) 
            { 
                return new GameBaseStateDodge(_gameplayHandler.gameObject); 
            }
            
            if (_gameplayHandler.IsGrounded)
            { 
                return new GameBaseStateLocomotion(_gameplayHandler.gameObject); 
            } 
            
            if (_gameplayHandler.IsFiring.Value)
            {
                return new GameBaseStateFire(_gameplayHandler.gameObject); 
            }
            return null;
        }
        
        public void OnCollisionEnter(Collision other) {}
        
        private void DoAir()
        {
            if (_gameplayHandler.HasJumped)
            {
                float xMove = _gameplayHandler.Move.x * Time.deltaTime * _gameplayHandler._moveSpeed * _gameplayHandler._airMultiplier;
                _gameplayHandler._rigidbody2D.linearVelocity = new Vector2(xMove, _gameplayHandler._rigidbody2D.linearVelocity.y);
            }
        }
    }
}