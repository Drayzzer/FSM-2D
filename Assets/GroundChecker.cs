using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public GameplayHandler GameplayHandler;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameplayHandler.IsJumping = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameplayHandler.IsJumping = false;
    }
}