using Godot;
namespace CharacterState.MovementFocused
{
    public class FallingState : AbstractMovementState
    {
        public FallingState(Character player) : base(player)
        {
            GD.print("Falling" + "State");
            player.ChangeHeight(1f);
        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            base.HandleEvent(ev);

            return this;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            base.PhysicsProcess(dt);
            Vector3 computedDirection = (player.movementVector.rotated(up, player.GetRotation().y)).normalized() * walkSpeed * dt;

            player.MoveAndSlide(computedDirection, new Vector3(0, 1, 0), 1f, 4, Mathf.PI / 4);

            //If the player is colliding with anything, change to stationary state
            KinematicCollision kc = player.MoveAndCollide(player.otherForces * dt);
            if (kc == null)
            {
                return this;
            }
            else
            {
                player.otherForces.y = -1; //Keep grounded - Super hacky?
                return new StandingState(player);
            }
        }
    }
}