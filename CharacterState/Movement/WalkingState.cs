using Godot;
namespace CharacterState.Movement
{
    public class WalkingState : AbstractMovementState
    {

        public WalkingState(CharacterStateManager csm) : base(csm)
        {
            csm.ChangeHeight(1f);
            GD.print("Walking" + "State");
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
            sharedState.ConsciousMovement(CalculateMovementVector(dt));

            if (!sharedState.WantsToMove())
            {
                return new StandingState(sharedState);
            }
            else if (sharedState.wantsToRun && sharedState.stamina >= runningStaminaThreshold)
            {
                return new RunningState(sharedState);
            }
            else if (sharedState.wantsToCrouch)
            {
                return new CrouchWalkingState(sharedState);
            }
            
            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(sharedState.UnconsciousMovement(sharedState.otherForces * dt));
        }
    }
}