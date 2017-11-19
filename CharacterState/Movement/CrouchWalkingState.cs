using Godot;
namespace CharacterState.Movement
{
    public class CrouchWalkingState : AbstractMovementState
    {

        public CrouchWalkingState(CharacterStateManager csm) : base(csm)
        {
            csm.ChangeHeight(0.5f);
            GD.print("CrouchWalking" + "State");
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            base.PhysicsProcess(dt);
            Vector3 computedDirection = CalculateMovementVector(dt);

            sharedState.ConsciousMovement(computedDirection);
            if (sharedState.movementVector.z == 0 && sharedState.movementVector.x == 0)
            {
                return new CrouchingState(sharedState);
            }
            else if (!sharedState.wantsToCrouch)
            {
                return new WalkingState(sharedState);
            }
            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(sharedState.UnconsciousMovement(sharedState.otherForces * dt));
        }
    }
}