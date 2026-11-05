using UnityEngine;

namespace Script.States
{
    public class GameBaseStateLocomotion: BaseState
    {
        public GameBaseStateLocomotion(GameObject host) : base (host){}
        public override void OnEnter(GameplayHandler context)
        {
            _gameplayHandler = context;
        }
        
        public override void OnUpdate()
        {
            DoLocomotion();
        }
        
        public override void OnExit(){}

        public override BaseState NextState()
        {
            if (_gameplayHandler.IsJumping.Value)
                return new GameBaseStateJump(_gameplayHandler.gameObject);

            if (_gameplayHandler.IsCrouching.Value)
                return new GameBaseStateCrouch(_gameplayHandler.gameObject);

            if (_gameplayHandler.IsDodging.Value)
                return new GameBaseStateDodge(_gameplayHandler.gameObject);
                
            if (_gameplayHandler.IsFiring.Value)
                return new GameBaseStateFire(_gameplayHandler.gameObject);

            if (_gameplayHandler.IsGuarding.Value)
                return new GameBaseStateGuard(_gameplayHandler.gameObject);

            return null;
        }

        public void OnCollisionEnter(Collision other) {}
        
        private void DoLocomotion()
        {
            float xMove = _gameplayHandler.Move.x * Time.deltaTime * _gameplayHandler._moveSpeed;
            _gameplayHandler._rigidbody2D.linearVelocity = new Vector2(xMove, _gameplayHandler._rigidbody2D.linearVelocity.y);
            _gameplayHandler.transform.localScale = new Vector3(1, 1, 1);

            // _gameplayHandler.CrouchingCollider.enabled = false;
            // _gameplayHandler.StandingCollider.enabled = true;
            
            // _crouchingCollider.SetActive(false);
            // _standingCollider.SetActive(true);
            
            _gameplayHandler._animator.SetFloat("Walk", 0);
        }
    }
}