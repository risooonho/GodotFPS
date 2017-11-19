using Godot;
namespace CharacterState.Movement
{
    public class WalkingState : AbstractMovementState
    {

        public WalkingState(Character player) : base(player)
        {
            player.ChangeHeight(1f);
            GD.print("Walking" + "State");
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
            Vector3 computedMovement = (player.movementVector.rotated(up, player.GetRotation().y)).normalized() * walkSpeed * dt;
            
            player.MoveAndSlide(computedMovement, up, 1f, 4, Mathf.PI / 4);

            if (player.movementVector.z == 0 && player.movementVector.x == 0)
            {
                return new StandingState(player);
            }
            else if (player.runHeld)
            {
                return new RunningState(player);
            }
            else if (player.crouchHeld)
            {
                return new CrouchWalkingState(player);
            }
            
            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(player.MoveAndCollide(player.otherForces * dt));
        }
    }
}