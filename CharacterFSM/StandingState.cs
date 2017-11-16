using Godot;
namespace CharacterState
{
    public class StandingState : BaseState
    {
        private bool nextMovementIsRun = false;

        public StandingState(Character player) : base(player)
        {
        }

        public override BaseState handleEvent(InputEvent ev)
        {
            base.handleEvent(ev);
            //Clamp axis to prevent weird movement
            player.movementVector.z = Mathf.min(1, Mathf.max(-1, player.movementVector.z));
            player.movementVector.x = Mathf.min(1, Mathf.max(-1, player.movementVector.x));

            if (ev.IsActionPressed("character_forward") || ev.IsActionReleased("character_backward"))
            {
                player.movementVector.z--;
                return nextMovementIsRun ? (BaseState) new RunningState(player) : new WalkingState(player);
            }
            else if (ev.IsActionReleased("character_forward") || ev.IsActionPressed("character_backward"))
            {
                player.movementVector.z++;
                return nextMovementIsRun ? (BaseState)new RunningState(player) : new WalkingState(player);
            }
            else if (ev.IsActionPressed("character_strafe_right") || ev.IsActionReleased("character_strafe_left"))
            {
                player.movementVector.x++;
                return nextMovementIsRun ? (BaseState)new RunningState(player) : new WalkingState(player);
            }
            else if (ev.IsActionReleased("character_strafe_right") || ev.IsActionPressed("character_strafe_left"))
            {
                player.movementVector.x--;
                return nextMovementIsRun ? (BaseState)new RunningState(player) : new WalkingState(player);
            }
            else if (ev.IsActionPressed("character_jump"))
            {
                player.verticalVelocity = jumpHeight;
            }
            else if (ev.IsActionPressed("character_run"))
            {
                nextMovementIsRun = true;
            }
            else if (ev.IsActionReleased("character_run"))
            {
                nextMovementIsRun = false;
            }
                
            return null;
        }

        public override BaseState physicsProcess(float dt)
        {
            player.verticalVelocity -= gravity * dt;
            Vector3 computedGravity = new Vector3(0, (player.verticalVelocity) * dt, 0);
            KinematicCollision kc = player.MoveAndCollide(computedGravity);
            return kc == null ? new FallingState(player) : null;
        }
    }
}