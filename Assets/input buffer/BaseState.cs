using input_buffer;
using Script;
using UnityEngine;

public abstract class BaseState
{
    protected GameplayHandler  _gameplayHandler;
    protected InputHandler _inputController;
    private float _duration;
    private float _timer;
    
    public bool IsFinished => _timer is > -1 and <= 0;

   
    protected BaseState(GameObject host)
    {
        _gameplayHandler = host.GetComponent<GameplayHandler>();
        _inputController = host.GetComponent<InputHandler>();
    }

    public abstract void OnEnter(GameplayHandler context);
    
    public abstract void OnUpdate();
    
    public abstract void OnExit();
    
    public abstract BaseState NextState();
}