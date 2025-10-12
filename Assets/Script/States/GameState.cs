namespace Script.States
{
    public class GameState
    {
        public GameStateLocomotion Locomotion { get; private set; } = new();
        public GameStateJump Jump { get; private set; } = new();
        public GameStateFire Fire { get; private set; } = new();
        public GameStateCrouch Crouch { get; private set; } = new();
        public GameStateDoAir DoAir { get; private set; } = new();
        public GameStateDodge Dodge { get; private set; } = new();
        public GameStateGuard Guard { get; private set; } = new();
        public GameStateAttack Attack { get; private set; } = new();
    }
}
