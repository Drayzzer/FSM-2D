using System;
using Script.States;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerStateManager: MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _airMultiplier;
        [SerializeField] private float _dodgeMultiplier;
        [SerializeField] private float _crouchMulitplier;
        [SerializeField] private float _fireCooldown;
        [SerializeField] private float _fireRecoil;

        [Header("References")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnRight;
        [SerializeField] private Transform _bulletSpawnLeft;
        [SerializeField] private Collider2D _crouchingCollider;
        [SerializeField] private Collider2D _standingCollider;

        [Header("Inputs")]
        public Vector2 Move;
        public bool IsWalking;
        public bool IsJumping;
        public bool IsCrouching;
        public bool IsFiring;
        public bool IsDodging;
        public bool IsGuarding;
        public bool IsDead;

        // ------ //

        public GameStateBase _currentState;
        public GameState States = new();

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        // ------ //

        /// <summary>
        /// Propriété: un genre de "super champ".
        /// Il est public donc accessible par les états qui ont le <c>PlayerStateManager</c> comme "contexte".
        /// La propriété est en lecture seule (=>). On ne peut pas assigner quelque chose de nouveau (=).
        /// </summary>
        public Rigidbody2D Rigidbody => _rigidbody;
        public Animator Animator => _animator;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public GameObject BulletPrefab => _bulletPrefab;
        public Transform BulletSpawnRight => _bulletSpawnRight;
        public Transform BulletSpawnLeft => _bulletSpawnLeft;
        public Collider2D CrouchingCollider => _crouchingCollider;
        public Collider2D StandingCollider => _standingCollider;

        public float MoveSpeed => _moveSpeed;
        public float JumpForce => _jumpForce;
        public float AirMultiplier => _airMultiplier;
        public float DodgeMultiplier => _dodgeMultiplier;
        public float CrouchMultiplier => _crouchMulitplier;
        public float FireCooldown => _fireCooldown;
        public float FireRecoil => _fireRecoil;

        public bool IsGrounded { get; set; }
        public float FireTimer { get; set; }
        public bool IsFlipped { get; set; }

        // ------- //

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _currentState = States.Locomotion;
            _currentState.EnterState(this);
        }

        private void Update()
        {
            UpdateAnimation();
            _currentState.UpdateState();
            FireTimer += Time.deltaTime;
        }

        public void SwitchState(GameStateBase newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState(this);
        }

        private void OnCollisionEnter(Collision other)
        {
            _currentState.OnCollisionEnter(other);
        }

        private void UpdateAnimation()
        {
            _animator.SetFloat("Walk", Mathf.Abs(Move.x));
            _animator.SetBool("IsGrounded", IsGrounded);
            _animator.SetBool("IsJumping", IsJumping);
            _animator.SetBool("IsDodging", IsDodging);
            _animator.SetBool("IsCrouching", IsCrouching);
            _animator.SetBool("IsGuarding", IsGuarding);
        }
    }
}
