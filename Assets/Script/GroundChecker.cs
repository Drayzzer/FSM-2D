using System;
using UnityEngine;

namespace Script
{
    public class GroundChecker : MonoBehaviour
    {
        private GameplayHandler _gameplayHandler;

        private void Awake()
        {
            _gameplayHandler = GetComponentInParent<GameplayHandler>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _gameplayHandler.IsGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _gameplayHandler.IsGrounded= false;
        }
    }
}