using Godot;
namespace CharacterState.Movement
{
    public class CrouchWalkingState : AbstractMovementState
    {

        public CrouchWalkingState(Character player) : base(player)
        {
            player.ChangeHeight(0.5f);
            GD.print("CrouchWalking" + "State");
        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            base.HandleEvent(ev);
            return this;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            base.PhysicsProcess(dt);
            Vector3 computedDirection = CalculateWalkVector(dt);

            player.MoveAndSlide(computedDirection, up, 1f, 4, Mathf.PI / 4);
            if (player.movementVector.z == 0 && player.movementVector.x == 0)
            {
                return new CrouchingState(player);
            }
            else if (!player.crouchHeld)
            {
                return new WalkingState(player);
            }
            //If the player isn't colliding with anything, change to falling state
            return CheckIfFalling(player.MoveAndCollide(player.otherForces * dt));
        }
    }
}