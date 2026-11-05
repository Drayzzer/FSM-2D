using UnityEngine;

namespace Script.States
{
    public class GameBaseStateGuard : BaseState
    {
        public GameBaseStateGuard(GameObject host) : base(host) {}

        public override void OnEnter(GameplayHandler context)
        {
            _gameplayHandler = context;
        }

        public override void OnUpdate(){}

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override BaseState NextState()
        {
            if (_gameplayHandler.IsGuarding.Value)
                return new GameBaseStateLocomotion(_gameplayHandler.gameObject);

            return null;
        }

        public  void OnCollisionEnter(Collision other){}
        
        private void DoGuard()
        {
            if (_gameplayHandler.IsGuarding.Value);
        }
    }
}
