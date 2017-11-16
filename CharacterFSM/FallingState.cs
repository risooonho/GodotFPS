using Godot;
namespace CharacterState
{
    public class FallingState : BaseState
    {

        public FallingState(Character player) : base(player)
        {
        }

        public override BaseState handleEvent(InputEvent ev)
        {
            return null;
        }

        public override BaseState physicsProcess(float dt)
        {
            return null;
        }
    }
}