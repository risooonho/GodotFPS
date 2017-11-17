using Godot;
namespace CharacterState.MovementFocused
{
    public class StandingState : AbstractMovementState
    {
        public StandingState(Character player) : base(player)
        {
            GD.print("Standing" + "State");
            player.ChangeHeight(1f);
        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            base.HandleEvent(ev);

            if (ev.IsActionPressed("character_jump"))
            {
                player.otherForces.y = jumpHeight;
            }
            return this;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            base.PhysicsProcess(dt);

            if (player.movementVector.x != 0 || player.movementVector.z != 0)
            {
                return new WalkingState(player);
            }
            else if (player.crouchHeld)
            {
                return new CrouchingState(player);
            }

            KinematicCollision kc = player.MoveAndCollide(player.otherForces * dt);
            if (kc != null)
            {
                player.otherForces.y = -gravity;
                return this;
            }
            else
            {
                return new FallingState(player);
            }
        }
    }
}