using UnityEngine;

namespace Script
{
    public class GroundChecker : MonoBehaviour
    {
        private GameplayHandler _animGrounded;
        public GameplayHandler GameplayHandler;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameplayHandler.IsGrounded = true;
            _animGrounded._animator.SetBool("IsGrounded", true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            GameplayHandler.IsGrounded= false;
            _animGrounded._animator.SetBool("IsGrounded", false);
        }
    }
}