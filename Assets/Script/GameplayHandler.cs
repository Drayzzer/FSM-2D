using System;
using System.Collections.Generic;
using input_buffer;
using Script.States;
using UnityEngine;

namespace Script
{
    public class GameplayHandler : MonoBehaviour
    {
        [Header("float Parameters")]
        public float _moveSpeed;
        public float _jumpForce;
        public float _airMultiplier;
        public float _crouchMultiplier;
        public float _dodgeMultiplier;
        public float Dodge;
        public float _fireCooldown;
        public float _fireRecoil;
        public float FireTimer;
        public InputHandler _inputController;
        [SerializeField] private float Recul;
        [SerializeField] private float InAirControl;
        [SerializeField] private PolygonCollider2D _hurtBox;
        

        [Header("Bool Parameters")]
        public bool IsGrounded;
        public Vector2 Move;
        public bool IsWalking;
        public bool IsDead;
        public bool HasJumped = false;
        
        [Header("Trigger Parameters")]
        public Trigger IsJumping;
        public Trigger IsCrouching;
        public Trigger IsFiring;
        public Trigger IsDodging;
        public Trigger IsGuarding;
        
        [Header("Component Parameters")]
        public GameObject _bulletPrefab;
        public Transform _bulletSpawnRight;
        public Transform _bulletSpawnLeft;
        public List<GameplayAction> _actions;

        [Header("Bool Parameters")]
        public Animator _animator;
        public Rigidbody2D _rigidbody2D;
        public SpriteRenderer _spriteRenderer;
        
        [Header("GameObject Parameters")]
        public GameObject CrouchingCollider;
        public GameObject StandingCollider;
        //[SerializeField] private GameObject _crouchingCollider;
        [SerializeField] private GameObject _standingCollider;
        [SerializeField] private GameObject _hitboxCollider;
        [SerializeField] private GameObject _bullet;
        
        
        private GameObject Bullet;
        private float Shot = 2;
        private BaseState _currentState;
        private bool HasFired = true;
        private Sprite _lastSprite;
        private readonly List<Vector2> _points = new();
        
        public int _actionIndex;
        public int actionIndex => _actionIndex;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _inputController =  GetComponent<InputHandler>();
            _currentState = new GameBaseStateLocomotion(gameObject);
        }
        
        public void Update()
        {
            SpriteOrientation();
            
            BaseState nextState = _currentState.NextState();
            if (nextState != null)
            {
                _currentState.OnExit();
                _currentState = nextState;
                _currentState.OnEnter(this);
            }
            
            Shot += Time.deltaTime;
            _animator.SetBool("IsGrounded", IsGrounded);
            _animator.SetBool("IsJumping", IsJumping.Value);
            _animator.SetBool("Attack", IsFiring.Value);
            _animator.SetBool("IsDodging", IsDodging.Value);
            _animator.SetBool("IsCrouching", IsCrouching.Value);
            _animator.SetBool("IsGuarding", IsGuarding.Value);
        }
        
        private void DoFire()
        {
            if (_bullet == null) return;

            if (Shot >= 3)
            {
                Bullet = Instantiate(_bullet);
                Bullet.SetActive(true);
                _rigidbody2D.AddForce(Vector2.left * Recul);
                Shot = 2;
            }
        }

        private void SpriteOrientation()
        {
            if (_rigidbody2D.linearVelocity.x > 0)
            {
                _spriteRenderer.flipX = false;
            }

            if (_rigidbody2D.linearVelocity.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
        
        private bool _hurtBoxActive;

        public void OnAttackActive() {
            _hurtBoxActive = true;
            _lastSprite = null;
            _hurtBox.enabled = true;
        }

        public void OnAttackFinished() {
            _hurtBoxActive = false;
            _hurtBox.enabled = false;
        }

        private void LateUpdate() {
            if (!_hurtBoxActive) return;
        
            Sprite sprite = _spriteRenderer.sprite;
            if (sprite == _lastSprite) return;
            _lastSprite = sprite;

            int shapeCount = sprite != null ? sprite.GetPhysicsShapeCount() : 0;
            _hurtBox.pathCount = shapeCount;

            for (int i = 0; i < shapeCount; i++) {
                sprite.GetPhysicsShape(i, _points);

                if (_spriteRenderer.flipX)
                    for (int p = 0; p < _points.Count; p++)
                        _points[p] = new Vector2(-_points[p].x, _points[p].y);

                _hurtBox.SetPath(i, _points);
            }
        }
    }
}