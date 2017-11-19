using Godot;

namespace CharacterState.Leaning
{
    class LeaningState : AbstractLeaningState
    {
        public LeaningState(Character player) : base(player)
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