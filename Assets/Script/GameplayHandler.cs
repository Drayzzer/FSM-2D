using System;
using UnityEngine;

public class GameplayHandler : MonoBehaviour
{

    private enum GameplayState
    {
        Locomotion,
        Jump,
        Fire,
        Crouch
    }

    [Header("Parameters")] [SerializeField]
    private float _moveSpeed;

    [SerializeField] public float _jumpForce;
    public bool groundChecker;

    [Header("Processed")] [SerializeField] private GameplayState _currentState = GameplayState.Locomotion;
    public bool _isGrounded;

    [Header("Inputs")] public Vector2 Move;
    public bool IsJumping;
    public bool IsCrouching;
    public bool IsFiring;
    


    private Rigidbody2D _rigidbody2D;
    private GameObject Bullet;
    [SerializeField] private GameObject _bullet;
    public float Recul;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Recul = 15;
    }

    public void Update()
    {
        switch (_currentState)
        {
            case GameplayState.Locomotion:
                if (IsJumping && _isGrounded) _currentState = GameplayState.Jump;
                if (IsCrouching) _currentState = GameplayState.Crouch;
                if (IsFiring) _currentState = GameplayState.Fire;
                DoLocomotion();
                break;
            case GameplayState.Jump:
                if (_isGrounded) _currentState = GameplayState.Locomotion;
                DoJump();
                break;
            case  GameplayState.Fire:
                if (IsFiring == false) _currentState = GameplayState.Locomotion;
                DoFire();
                break;
            case GameplayState.Crouch:
                if (IsCrouching == false) _currentState = GameplayState.Locomotion;
                if (IsCrouching && IsJumping) _currentState = GameplayState.Jump;
                DoCrouch();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void DoLocomotion()
    {
        float xMove = Move.x * Time.deltaTime * _moveSpeed;
        transform.Translate(xMove, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }
    private void DoJump()
    {
        groundChecker = true;
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }
    }
    private void DoCrouch()
    {
        float xMove = Move.x * Time.deltaTime * _moveSpeed / 2;
        transform.Translate(xMove, 0, 0);
        transform.localScale = new Vector3 (1, 0.5f, 1);
        
    }
    private void DoFire()
    {
        Bullet = Instantiate(_bullet);
        Bullet.SetActive(true);
        _rigidbody2D.AddForce(Vector2.left * Recul);
        Bullet.transform.position = gameObject.transform.position;
    }
}