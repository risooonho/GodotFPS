using Godot;
namespace CharacterState.MovementFocused
{
    public class RunningState : AbstractMovementState
    {
        public RunningState(Character player) : base(player)
        {
            GD.print("Running" + "State");
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
            Vector3 computedDirection = (player.movementVector.rotated(up, player.GetRotation().y)).normalized() * walkSpeed * runMultiplier * dt;

            player.MoveAndSlide(computedDirection, up, 1f, 4, Mathf.PI / 4);

            if (player.movementVector.z == 0 && player.movementVector.x == 0)
            {
                return new StandingState(player);
            }
            else if (!player.runHeld)
            {
                return new WalkingState(player);
            }
            
            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(player.MoveAndCollide(player.otherForces * dt));
        }
    }
}