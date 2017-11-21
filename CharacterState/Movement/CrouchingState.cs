using Godot;

namespace CharacterState.Movement
{
    public class CrouchingState : AbstractMovementState
    {
        private const float crouchHeight = 0.5f;

        public CrouchingState(CharacterStateManager csm) : base(csm)
        {
            GD.print("Crouching" + "State");
            csm.ChangeHeight(crouchHeight);
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            base.PhysicsProcess(dt);
            if (sharedState.WantsToMove())
            {
                return new CrouchWalkingState(sharedState);
            }
            else if (!sharedState.wantsToCrouch)
            {
                return new StandingState(sharedState);
            }
            
            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(sharedState.UnconsciousMovement(sharedState.otherForces * dt));
        }
    }
}