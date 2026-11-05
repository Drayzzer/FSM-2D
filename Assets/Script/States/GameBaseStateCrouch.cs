using UnityEngine;

namespace Script.States
{
    public class GameBaseStateCrouch: BaseState
    {
        public GameBaseStateCrouch(GameObject host) : base (host){}
        
        public override void OnEnter(GameplayHandler context)
        {
            _gameplayHandler = context;

            float xMove = _gameplayHandler.Move.x * Time.deltaTime * _gameplayHandler._moveSpeed * _gameplayHandler._crouchMultiplier;
            _gameplayHandler._rigidbody2D.linearVelocity = new Vector2(xMove, _gameplayHandler._rigidbody2D.linearVelocity.y);

            // if (!_gameplayHandler.CrouchingCollider.enabled)
            // {
            //     _gameplayHandler.CrouchingCollider.enabled = true;
            //     _gameplayHandler.StandingCollider.enabled = false;
            // }
        }
        
        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            // _gameplayHandler.CrouchingCollider.enabled = false;
            // _gameplayHandler.StandingCollider.enabled = true;
        }

        public override BaseState NextState()
        {
            throw new System.NotImplementedException();
        }
        
        public void OnCollisionEnter(Collision other){}
        
        // private void DoCrouch()
        // {
        //     float xMove = Move.x * Time.deltaTime * _moveSpeed * _crouchMultiplier;
        //     _rigidbody2D.linearVelocity = new Vector2(xMove, _rigidbody2D.linearVelocity.y);
        //     if (CrouchingCollider != true)
        //     {
        //         _crouchingCollider.SetActive(true);
        //         _standingCollider.SetActive(false);
        //     }
        // }
    }
}
