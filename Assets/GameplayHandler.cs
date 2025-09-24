using System;
using UnityEngine;

public class GameplayHandler : MonoBehaviour
{
    
    private enum GameplayState
    {
        Locomotion,
        Jump,
        Fire,
        Crounch
    }
    
    [Header("Parameters")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] public float _jumpForce;
    public bool  groundChecker;
    
    [Header("Processed")]
    [SerializeField] private GameplayState _currentState = GameplayState.Locomotion;
    public bool _isGrounded;
    
    [Header("Inputs")]
    public Vector2 Move;
    public bool IsJumping;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        switch (_currentState)
        {
            case GameplayState.Locomotion:
                if (IsJumping && _isGrounded) _currentState = GameplayState.Jump;
                DoLocomotion();
                break;
            case GameplayState.Jump:
                if (_isGrounded) _currentState = GameplayState.Locomotion;
                DoJump();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void DoLocomotion()
    {
        float xMove = Move.x * Time.deltaTime * _moveSpeed;
        transform.Translate(xMove, 0, 0);
    }
    
    private void DoJump()
    {
        groundChecker = true;
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }
    }
}