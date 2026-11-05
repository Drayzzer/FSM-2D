using UnityEngine;

namespace Script
{
    [CreateAssetMenu(fileName = "GameplayAction", menuName = "Script/GameplayHandler")]
    public class GameplayAction : ScriptableObject
    {
        public AnimatorOverrideController animatorOverrideController;
        public AnimationClip Clip;
        
        public int Damage;
        public float Speed;
    }
}