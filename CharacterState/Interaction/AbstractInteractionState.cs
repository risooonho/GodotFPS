using Godot;

namespace CharacterState.Interaction
{
    class AbstractInteractionState : AbstractState
    {
        public AbstractInteractionState(Character player) : base(player)
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