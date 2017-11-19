using Godot;

namespace CharacterState.Leaning
{
    public class AbstractLeaningState : AbstractState
    {
        public AbstractLeaningState(CharacterStateManager csm) : base(csm)
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