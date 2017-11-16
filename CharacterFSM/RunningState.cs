using Godot;
namespace CharacterState
{
    public class RunningState : AbstractState
    {
        public RunningState(Character player) : base(player)
        {
            GD.print("Running" + "State");
        }

        public override AbstractState handleEvent(InputEvent ev)
        {
            base.handleEvent(ev);
            //Clamp axis to prevent weird movement
            player.movementVector.z = Mathf.min(1, Mathf.max(-1, player.movementVector.z));
            player.movementVector.x = Mathf.min(1, Mathf.max(-1, player.movementVector.x));

            if (ev.IsActionPressed("character_forward") || ev.IsActionReleased("character_backward"))
            {
                player.movementVector.z--;
            }
            else if (ev.IsActionReleased("character_forward") || ev.IsActionPressed("character_backward"))
            {
                player.movementVector.z++;
            }
            else if (ev.IsActionPressed("character_strafe_right") || ev.IsActionReleased("character_strafe_left"))
            {
                player.movementVector.x++;
            }
            else if (ev.IsActionReleased("character_strafe_right") || ev.IsActionPressed("character_strafe_left"))
            {
                player.movementVector.x--;
            }
            else if (ev.IsActionPressed("character_jump"))
            {
                player.verticalVelocity = jumpHeight;
            }
            else if (ev.IsActionReleased("character_run"))
            {
                return new WalkingState(player);
            }

            return this;
        }

        public override AbstractState physicsProcess(float dt)
        {
            player.verticalVelocity -= gravity * dt;
            Vector3 computedGravity = new Vector3(0, (player.verticalVelocity) * dt, 0);
            Vector3 computedDirection = (player.movementVector.rotated(up, player.GetRotation().y)).normalized() * walkSpeed * runMultiplier * dt;

            player.MoveAndSlide(computedDirection, up, 1f, 4, Mathf.PI / 4);

            if (player.movementVector.z == 0 && player.movementVector.x == 0)
            {
                return new StandingState(player);
            }
            KinematicCollision kc = player.MoveAndCollide(computedGravity);
            //If the player isn't colliding with anything, change to falling state
            if (kc != null)
            {
                player.verticalVelocity = -gravity;
                return this;
            }
            else
            {
                return new FallingState(player);
            }
        }
    }
}