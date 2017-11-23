using Godot;

namespace CharacterState.Movement
{
    public abstract class AbstractMovementState : AbstractState
    {
        public Vector3 up = new Vector3(0, 1, 0);

        protected const float gravity = 9.8f;
        protected const float jumpHeight = 5f;
        protected const float runMultiplier = 3f;
        protected const float walkSpeed = 100f;
        protected const float mouseSensitivity = 0.005f;

        protected const float maxStamina = 10f;
        protected const float runningStaminaThreshold = 5f;

        public AbstractMovementState(CharacterStateManager csm) : base(csm)
        {

        }
        public override AbstractState HandleEvent(InputEvent ev)
        {
            if (ev is InputEventMouseMotion)
            {
                HandleMouse((InputEventMouseMotion)ev);
            }
            else if (!HandleMovementEvents(ev))
            {

            }
            return this;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            sharedState.otherForces.y -= gravity * dt;
            CalculateStamina(dt);
            return this;
        }

        private bool HandleMovementEvents(InputEvent ev)
        {
            if (ev.IsActionPressed("character_forward") || ev.IsActionReleased("character_backward"))
            {
                sharedState.movementVector.z--;
            }
            else if (ev.IsActionReleased("character_forward") || ev.IsActionPressed("character_backward"))
            {
                sharedState.movementVector.z++;
            }
            else if (ev.IsActionPressed("character_strafe_right") || ev.IsActionReleased("character_strafe_left"))
            {
                sharedState.movementVector.x++;
            }
            else if (ev.IsActionReleased("character_strafe_right") || ev.IsActionPressed("character_strafe_left"))
            {
                sharedState.movementVector.x--;
            }
            else if (ev.IsActionPressed("character_run"))
            {
                sharedState.wantsToRun = true;
            }
            else if (ev.IsActionReleased("character_run"))
            {
                sharedState.wantsToRun = false;
            }
            else if (ev.IsActionPressed("character_crouch"))
            {
                sharedState.wantsToCrouch = true;
            }
            else if (ev.IsActionReleased("character_crouch"))
            {
                sharedState.wantsToCrouch = false;
            }
            else
            {
                return false;   //Event isn't handled
            }

            return true;    //Event is handled
        }

        private void HandleMouse(InputEventMouseMotion iemm)
        {
            Vector2 motion = -iemm.GetRelative();
            sharedState.RotateCamera(new Vector3 { x = motion.y, y = motion.x } * mouseSensitivity);
        }

        protected Vector3 CalculateMovementVector(float dt)
        {
            Vector3 finalMovement = sharedState.movementVector.rotated(up, sharedState.GetCharacterRotation().y);
            finalMovement = finalMovement.normalized();
            finalMovement *= walkSpeed * dt;
            return finalMovement;
        }

        protected AbstractState CheckIfFalling(KinematicCollision kc)
        {
            if (kc == null)
            {
                return new FallingState(sharedState);
            }
            sharedState.otherForces.y = -5; //Keep player grounded - This is kinda hacky
            return this;
        }

        protected virtual void CalculateStamina(float dt)
        {
            sharedState.SetStamina(sharedState.GetStamina() + dt);
            if (sharedState.GetStamina() > maxStamina)
            {
                sharedState.SetStamina(maxStamina);
            }
        }

    }
}
