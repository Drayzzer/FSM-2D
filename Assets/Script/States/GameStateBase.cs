using UnityEngine;

namespace Script.States
{
    // GameStateBase est "abstraite", ça veut dire qu'on ne peut pas créer d'instance (pas de "new()")
    public abstract class GameStateBase
    {
        // "protected" = Entre le privé et le public, permet d'être accessible par les classes enfants
        protected PlayerStateManager _player;

        // Comme la classe est abstraite, toutes les méthodes doivent l'être aussi.
        // Pour utiliser les méthodes dans les états héritiers, je dois remplacer "abstract" par "override".
        public abstract void EnterState(PlayerStateManager context);

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void OnCollisionEnter(Collision other);
    }
}
