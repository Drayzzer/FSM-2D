using UnityEngine;

namespace Script
{
    public class GroundChecker : MonoBehaviour
    {
        public GameplayHandler GameplayHandler;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameplayHandler._isGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            GameplayHandler._isGrounded= false;
        }
    }
}