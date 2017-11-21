using Godot;

namespace CharacterState.Interaction
{
    public class InteractionState : AbstractInteractionState
    {
        public InteractionState(CharacterStateManager csm) : base(csm)
        {

        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            return this;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            return this;
        }
    }
}