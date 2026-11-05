using System;
using UnityEngine;

namespace Script
{
    public class GroundChecker : MonoBehaviour
    {
        private GameplayHandler _player;

        private void Awake()
        {
            _player = FindFirstObjectByType<GameplayHandler>(FindObjectsInactive.Include);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _player.IsGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _player.IsGrounded = false;
        }
    }
}
