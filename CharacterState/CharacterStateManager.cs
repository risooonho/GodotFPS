using Godot;

using CharacterState.Movement;
using CharacterState.Leaning;
using CharacterState.Interaction;

namespace CharacterState
{
    class StateManager
    {
        Character character;

        AbstractMovementState movementState;
        AbstractLeaningState leaningState;
        AbstractInteractionState interactionState;

        public Vector3 movementVector = new Vector3();
        public Vector3 otherForces = new Vector3();

        public bool wantsToCrouch = false;
        public bool wantsToRun = false;

        public StateManager(Character character)
        {
            this.character = character;

            movementState = new StandingState(character);
            leaningState = new LeaningState(character);
            interactionState = new InteractionState(character);
        }

        public void _Input(InputEvent ev)
        {
            movementState.HandleEvent(ev);
            leaningState.HandleEvent(ev);
            interactionState.HandleEvent(ev);
        }

        public void _PhysicsProcess(float dt)
        {
            movementState.PhysicsProcess(dt);
            leaningState.PhysicsProcess(dt);
            interactionState.PhysicsProcess(dt);
        }

        public bool wantsToMove()
        {
            return !(movementVector.z == 0 && movementVector.x == 0);
        }
    }
}
