using System;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayHandler : MonoBehaviour
{
   private enum GameplayState
   {
       Locomotion,
       Jump,
       Fire,
       Crouch,
       DoAir,
       Dodge,
       Guard,
       Attack
   }
   [Header("Parameters")]
   [SerializeField] private float _moveSpeed;
   // permet de lui ajouter une vitesse de déplacement
   [SerializeField] public float _jumpForce;
   // permet de lui ajouter une force pour le saut
   [SerializeField] private float _airMultiplier;
   [SerializeField] private float _crouchMultiplier;
   [SerializeField] private float _dodgeMultiplier;
   
   [Header("Processed")]
   [SerializeField] private GameplayState _currentState = GameplayState.Locomotion;
   public bool IsGrounded;
   
   [Header("Inputs")]
   public Vector2 Move;
   public bool IsWalking;
   public bool IsJumping;
   public bool IsCrouching;
   public bool IsFiring;
   public bool IsDodging;
   public bool IsGuarding;
   private bool HasFired = true;
   private float Shot = 2;
   private bool HasJumped = false;
   
   public Animator _animator;
   private Rigidbody2D _rigidbody2D;
   private SpriteRenderer _spriteRenderer;
   private GameObject Bullet;
   private GameObject CrouchingCollider;
   private GameObject StandingCollider;
   [SerializeField] private GameObject _crouchingCollider;
   [SerializeField] private GameObject _standingCollider;
   [SerializeField] private GameObject _bullet;
   [SerializeField] private float Recul;
   // un champ qui me permet de créer un recul quand mon perso tire
   [SerializeField] private float InAirControl;
   [SerializeField] private float Dodge;
   
   private void Awake()
   {
       _rigidbody2D = GetComponent<Rigidbody2D>();
       _animator = GetComponent<Animator>();
       _spriteRenderer = GetComponent<SpriteRenderer>();
   }
   public void Update()
   {
       Shot += Time.deltaTime;
       _animator.SetBool("IsGrounded", IsGrounded);
       _animator.SetBool("IsJumping", IsJumping);
       _animator.SetBool("Attack", IsFiring);
       _animator.SetBool("IsDodging", IsDodging);
       _animator.SetBool("IsCrouching", IsCrouching);
       _animator.SetBool("IsGuarding", IsGuarding);
      // _animator.SetBool("Dead", );
      // _animator.SetBool("IsDamaged"),
       
       switch (_currentState)
       {
           case GameplayState.Locomotion: 
               // Condition
               if (IsJumping && IsGrounded) _currentState = GameplayState.Jump;
               if (IsCrouching) _currentState = GameplayState.Crouch;
               if (IsFiring) _currentState = GameplayState.Fire;
               if (IsDodging) _currentState = GameplayState.Dodge;
               if (IsGuarding) _currentState = GameplayState.Guard;
               HasFired = true;
              
               
               DoLocomotion();
               // State
               break;
           
           case GameplayState.Jump:
               if (!IsGrounded) _currentState = GameplayState.Locomotion;
               // Condition
               if (!HasJumped)
               {
                   DoJump();
                   // State
                   HasJumped = true;
               }
               else
               {
                  DoAir();
                   HasJumped = false;
               }
               break;
           
           case GameplayState.DoAir:
               if (HasJumped) _currentState = GameplayState.Locomotion;
               
               DoAir();
               break;
           
           case GameplayState.Fire:
               if (!HasFired)
               {
                   _currentState = GameplayState.Locomotion;
               }
               else
               {
                   HasFired = false;
                   
                   DoFire();
               }
               break;
           
           case GameplayState.Crouch:
               if (!IsCrouching) _currentState = GameplayState.Locomotion;
               if (IsCrouching && IsJumping) _currentState = GameplayState.Jump;
               DoCrouch();
               break;
           
           case GameplayState.Dodge:
               
               if (!IsDodging) _currentState = GameplayState.Locomotion;

               DoDodge();
               break;
           
           case GameplayState.Guard:
               
               if (!IsGuarding) _currentState = GameplayState.Locomotion;
                   
               DoGuard();
               break;
           default:
               throw new ArgumentOutOfRangeException();
       }
   }
   private void DoLocomotion()
   {
       float xMove = Move.x * Time.deltaTime * _moveSpeed;
       _rigidbody2D.linearVelocity = new Vector2(xMove, _rigidbody2D.linearVelocity.y);
       transform.localScale = new Vector3(1, 1, 1);
       _crouchingCollider.SetActive(false);
       _standingCollider.SetActive(true);
       _animator.SetFloat("Walk", 0);
       
       if (_rigidbody2D.linearVelocity.x > 0)
       {
           _spriteRenderer.flipX = false;
       }

       if (_rigidbody2D.linearVelocity.x < 0)
       {
           _spriteRenderer.flipX = true;
       }
   }
   
   private void DoAir()
   {
       if (HasJumped)
       {
           float xMove = Move.x * Time.deltaTime * _moveSpeed * _airMultiplier;
           _rigidbody2D.linearVelocity = new Vector2(xMove, _rigidbody2D.linearVelocity.y);
       }
   }
   
   private void DoJump()
   {
       _rigidbody2D.AddForce(transform.up * _jumpForce);
   }
   
   private void DoCrouch()
   {
       float xMove = Move.x * Time.deltaTime * _moveSpeed * _crouchMultiplier;
       _rigidbody2D.linearVelocity = new Vector2(xMove, _rigidbody2D.linearVelocity.y);
      if (CrouchingCollider != true)
      {
          _crouchingCollider.SetActive(true);
          _standingCollider.SetActive(false);
      }
   }
   private void DoFire()
   {
       if (Shot >= 3)
       {
           Bullet = Instantiate(_bullet);
           // permet de duppliquer le gameobject
           Bullet.SetActive(true);
           //setactive permet de mettre active le gameobject qui est cacher ( qui inactif )
           _rigidbody2D.AddForce(Vector2.left * Recul);
           // j'applique une force a mon projectile qui fait reculer mon personnage
           Shot = 2;
       }
   }

   private void DoDodge()
   {
       Dodge = Move.x * Time.deltaTime * _moveSpeed * _dodgeMultiplier;
       _rigidbody2D.linearVelocity = new Vector2(Dodge, _rigidbody2D.linearVelocity.y);
   }

   private void DoGuard()
   {

   }
}
