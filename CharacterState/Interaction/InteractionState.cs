using Godot;

namespace CharacterState.Interaction
{
    class InteractionState : AbstractInteractionState
    {
        public InteractionState(Character player) : base(player)
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