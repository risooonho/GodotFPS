using Godot;

namespace CharacterState.Leaning
{
    public class LeaningState : AbstractLeaningState
    {
        public LeaningState(CharacterStateManager csm) : base(csm)
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