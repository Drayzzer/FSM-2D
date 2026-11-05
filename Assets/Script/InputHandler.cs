using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class InputHandler : MonoBehaviour
    {
        public static event Action<bool> OnInputDeviceChanged;

        private PlayerInput _playerInput;
        private GameplayHandler _player;
        
        private bool _isControllerConnected;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _player = FindFirstObjectByType<GameplayHandler>();
            if (_playerInput == null) throw new NullReferenceException("PlayerInputManager is null");
        }

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;

            DetectCurrentInputDevice();
        }
        private void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
            
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
        private void OnMove(InputValue value)
        {
            _player.Move = value.Get<Vector2>();
        }
        private void OnJump(InputValue value)
        {
            if (value.isPressed) _player.IsJumping.launch();
        }
        private void OnCrouch(InputValue value)
        {
            if (value.isPressed) _player.IsCrouching.launch();
        }
        private void OnFire(InputValue value)
        {
            if (value.isPressed) _player.IsFiring.launch();
        } 
        private void OnDodge(InputValue value)
        {
            if (value.isPressed) _player.IsDodging.launch();
        }
        private void OnGuard(InputValue value)
        {
            if (value.isPressed) _player.IsGuarding.launch();
        }
    }
}
