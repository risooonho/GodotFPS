using Godot;
namespace CharacterState.Movement
{
    public class RunningState : AbstractMovementState
    {
        public RunningState(CharacterStateManager csm) : base(csm)
        {
            GD.print("Running" + "State");
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

            if (sharedState.GetStamina() == 0)
            {
                return new WalkingState(sharedState);
            }

            sharedState.ConsciousMovement(CalculateMovementVector(dt) * runMultiplier);
            if (!sharedState.WantsToMove())
            {
                return new StandingState(sharedState);
            }
            else if (!sharedState.wantsToRun)
            {
                return new WalkingState(sharedState);
            }
            
            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(sharedState.UnconsciousMovement(sharedState.otherForces * dt));
        }

        protected override void CalculateStamina(float dt)
        {
            sharedState.SetStamina(sharedState.GetStamina() - dt);
            if (sharedState.GetStamina() < 0)
            {
                sharedState.SetStamina(0);
            }
        }
    }
}