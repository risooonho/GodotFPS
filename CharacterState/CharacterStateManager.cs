using Godot;

using CharacterState.Movement;
using CharacterState.Leaning;
using CharacterState.Interaction;

namespace CharacterState
{
    public class CharacterStateManager
    {
        private Character character;

        private HUD hud;

        public AbstractMovementState movementState;
        public AbstractLeaningState leaningState;
        public AbstractInteractionState interactionState;

        public Vector3 movementVector = new Vector3();
        public Vector3 otherForces = new Vector3();

        public bool wantsToCrouch = false;
        public bool wantsToRun = false;

        private float health = 100f;
        private float stamina = 0f;

        public CharacterStateManager(Character character)
        {
            this.character = character;

            hud = (HUD) character.GetNode("HUD");

            movementState = new StandingState(this);
            leaningState = new NoLeaningState(this);
            interactionState = new InteractionState(this);
        }

        public void _Input(InputEvent ev)
        {
            movementState = (AbstractMovementState)movementState.HandleEvent(ev);
            leaningState = (AbstractLeaningState)leaningState.HandleEvent(ev);
            interactionState = (AbstractInteractionState)interactionState.HandleEvent(ev);
        }

        public void _PhysicsProcess(float dt)
        {
            movementState = (AbstractMovementState) movementState.PhysicsProcess(dt);
            leaningState = (AbstractLeaningState)leaningState.PhysicsProcess(dt);
            interactionState = (AbstractInteractionState)interactionState.PhysicsProcess(dt);
        }

        public bool WantsToMove()
        {
            return movementVector.z != 0 || movementVector.x != 0;
        }

        public float GetStamina()
        {
            return stamina;
        }

        public void SetStamina(float stamina)
        {
            this.stamina = stamina;
            hud.SetStamina(this.stamina);
        }

        public float GetHealth()
        {
            return health;
        }

        public void SetHealth(float health)
        {
            this.health = health;
            hud.SetHealth(this.health);
        }

        public Vector3 ConsciousMovement(Vector3 movement)
        {
            return character.ConsciousMovement(movement);
        }

        public KinematicCollision UnconsciousMovement(Vector3 movement)
        {
            return character.UnconsciousMovement(movement);
        }

        public void ChangeHeight(float height)
        {
            character.ChangeHeight(height);
        }
        
        public Vector3 GetCharacterRotation()
        {
            return character.GetCharacterRotation();
        }

        public void RotateCamera(Vector3 rotation)
        {
            character.RotateCamera(rotation);
        }

        public bool NoStandingSpace()
        {
            return character.IsOnCeiling();
        }

        public void LeanAtDegrees(float angle)
        {
            character.LeanAtDegrees(angle);
        }
    }
}
