using Godot;

namespace CharacterState.Leaning
{
    class AbstractLeaningState : AbstractState
    {
        public AbstractLeaningState(Character player) : base(player)
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