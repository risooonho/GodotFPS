using Godot;

namespace CharacterState.Interaction
{
    public class AbstractInteractionState : AbstractState
    {
        public AbstractInteractionState(CharacterStateManager csm) : base(csm)
        {

        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            return null;
        }

        public override AbstractState PhysicsProcess(float dt)
        {
            return null;
        }
    }
}