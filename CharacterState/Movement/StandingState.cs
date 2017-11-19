using Godot;
namespace CharacterState.Movement
{
    public class StandingState : AbstractMovementState
    {
        public StandingState(CharacterStateManager csm) : base(csm)
        {
            GD.print("Standing" + "State");
            csm.ChangeHeight(1f);
        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            base.HandleEvent(ev);

            if (ev.IsActionPressed("character_jump"))
            {
                sharedState.otherForces.y = jumpHeight;
            }
            return this;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            base.PhysicsProcess(dt);

            sharedState.ConsciousMovement(new Vector3());
            if (sharedState.NoStandingSpace())
            {
                sharedState.wantsToCrouch = true;
                return new CrouchingState(sharedState);
            }

            if (sharedState.WantsToMove())
            {
                return new WalkingState(sharedState);
            }
            else if (sharedState.wantsToCrouch)
            {
                return new CrouchingState(sharedState);
            }

            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(sharedState.UnconsciousMovement(sharedState.otherForces * dt));
        }
    }
}