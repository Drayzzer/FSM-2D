using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class InputHandler : MonoBehaviour
    {
        public static event Action<bool> OnInputDeviceChanged;

        private PlayerInput _playerInput;
        private PlayerStateManager _player;
        private bool _isControllerConnected;

        private void Awake()
        {
            // Tip: GetComponent() ne cherche que sur l'objet où le script se trouve
            _playerInput = GetComponent<PlayerInput>();
            // FindFirstObjectByType() cherche à travers tout les objets pour trouver le composant.
            _player = FindFirstObjectByType<PlayerStateManager>();
            if (_playerInput == null) throw new NullReferenceException("PlayerInputManager is null");
        }

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;

            // Bind input actions
            _playerInput.actions["Move"].performed += OnMove;
            _playerInput.actions["Move"].canceled += OnMove;
            _playerInput.actions["Jump"].performed += OnJump;
            _playerInput.actions["Jump"].canceled += OnJump;
            _playerInput.actions["Crouch"].performed += OnCrouch;
            _playerInput.actions["Crouch"].canceled += OnCrouch;
            _playerInput.actions["Fire"].performed += OnFire;
            _playerInput.actions["Fire"].canceled += OnFire;
            _playerInput.actions["Dodge"].performed += OnDodge;
            _playerInput.actions["Dodge"].canceled += OnDodge;
            _playerInput.actions["Guard"].performed += OnGuard;
            _playerInput.actions["Guard"].canceled += OnGuard;
            DetectCurrentInputDevice();
        }
        private void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;

            // Unbind input actions
            _playerInput.actions["Move"].performed -= OnMove;
            _playerInput.actions["Move"].canceled -= OnMove;
            _playerInput.actions["Jump"].performed -= OnJump;
            _playerInput.actions["Jump"].canceled -= OnJump;
            _playerInput.actions["Crouch"].performed -= OnCrouch;
            _playerInput.actions["Crouch"].canceled -= OnCrouch;
            _playerInput.actions["Fire"].performed -= OnFire;
            _playerInput.actions["Fire"].canceled -= OnFire;
            _playerInput.actions["Dodge"].performed -= OnDodge;
            _playerInput.actions["Dodge"].canceled -= OnDodge;
            _playerInput.actions["Guard"].performed -= OnGuard;
            _playerInput.actions["Guard"].canceled -= OnGuard;
        }
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
            {
                DetectCurrentInputDevice();
            }
        }
        private void DetectCurrentInputDevice()
        {
            _isControllerConnected = Gamepad.all.Count > 0;
            OnInputDeviceChanged?.Invoke(_isControllerConnected);

            Debug.Log(_isControllerConnected
                ? "Controller connected: Switching to Gamepad controls."
                : "No controller connected: Switching to Keyboard/Mouse controls.");
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            _player.Move = context.ReadValue<Vector2>();
        }
        private void OnJump(InputAction.CallbackContext context)
        {
            _player.IsJumping = context.ReadValueAsButton();
        }
        private void OnCrouch(InputAction.CallbackContext context)
        {
            _player.IsCrouching = context.ReadValueAsButton();
        }
        private void OnFire(InputAction.CallbackContext context)
        {
            _player.IsFiring = context.ReadValueAsButton();
        }
        private void OnDodge(InputAction.CallbackContext context)
        {
            _player.IsDodging = context.ReadValueAsButton();
        }
        private void OnGuard(InputAction.CallbackContext context)
        {
            _player.IsGuarding = context.ReadValueAsButton();
        }
    }
}
