using Godot;
namespace CharacterState.Movement
{
    public class FallingState : AbstractMovementState
    {
        public FallingState(CharacterStateManager csm) : base(csm)
        {
            GD.print("Falling" + "State");
            csm.ChangeHeight(1f);
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            base.PhysicsProcess(dt);

            sharedState.ConsciousMovement(CalculateMovementVector(dt));

            //If the player is colliding with anything, change to stationary state
            KinematicCollision kc = sharedState.UnconsciousMovement(sharedState.otherForces * dt);
            if (kc != null)
            {
                sharedState.otherForces.y = -5; //Keep grounded - Super hacky?
                return new StandingState(sharedState);
            }
            return this;
        }
    }
}