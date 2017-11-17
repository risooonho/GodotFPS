using Godot;
using CharacterState;

namespace CharacterState.MovementFocused
{
    public abstract class AbstractMovementState : AbstractState
    {
        public AbstractMovementState(Character player) : base(player)
        {

        }
        public override AbstractState HandleEvent(InputEvent ev)
        {
            if (ev is InputEventMouseMotion)
            {
                HandleMouse((InputEventMouseMotion)ev);
            }
            else if (HandleMovementEvents(ev))
            {

            }
            return this;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            player.otherForces.y -= gravity * dt;
            return null;
        }

        public bool HandleMovementEvents(InputEvent ev)
        {
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
            else if (ev.IsActionPressed("character_run"))
            {
                player.runHeld = true;
            }
            else if (ev.IsActionReleased("character_run"))
            {
                player.runHeld = false;
            }
            else if (ev.IsActionPressed("character_crouch"))
            {
                player.crouchHeld = true;
            }
            else if (ev.IsActionReleased("character_crouch"))
            {
                player.crouchHeld = false;
            }
            else
            {
                return false;   //Event isn't handled
            }

            return true;    //Event is handled
        }

        public void HandleMouse(InputEventMouseMotion iemm)
        {
            Vector2 motion = -iemm.GetRelative();
            player.RotateY(motion.x * mouseSensitivity);

            float xrot = Mathf.max(Mathf.min(motion.y * mouseSensitivity + player.viewCamera.GetRotation().x, Mathf.PI / 2), -Mathf.PI / 2);
            player.viewCamera.SetRotation(new Vector3(xrot, 0, 0));
        }

        protected Vector3 CalculateWalkVector(float dt)
        {
            return (player.movementVector.rotated(up, player.GetRotation().y)).normalized() * walkSpeed * dt;
        }

        protected bool PlayerWantsToMove()
        {
            return player.movementVector.x != 0 || player.movementVector.z != 0;
        }

        protected AbstractState CheckIfFalling(KinematicCollision kc)
        {
            if (kc == null)
            {
                return new FallingState(player);
            }
            player.otherForces.y = -1;   //Keep player grounded
            return this;
        }
    }
}
