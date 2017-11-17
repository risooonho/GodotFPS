using Godot;

namespace CharacterState.MovementFocused
{
    public class CrouchingState : AbstractMovementState
    {
        private const float crouchHeight = 0.5f;

        public CrouchingState(Character player) : base(player)
        {
            GD.print("Crouching" + "State");
            player.ChangeHeight(crouchHeight);
        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            base.HandleEvent(ev);
            return this;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            base.PhysicsProcess(dt);
            if (PlayerWantsToMove())
            {
                return new CrouchWalkingState(player);
            }
            else if (!player.crouchHeld)
            {
                return new StandingState(player);
            }
            
            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(player.MoveAndCollide(player.otherForces * dt));
        }
    }
}