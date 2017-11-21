using Godot;

namespace CharacterState.Leaning
{
    public class AbstractLeaningState : AbstractState
    {
        protected float angle = 0f;
        protected float leanAngle = 20f;

        public AbstractLeaningState(CharacterStateManager sharedState) : base(sharedState)
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