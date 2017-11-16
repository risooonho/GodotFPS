using Godot;
namespace CharacterState
{
    public class CrouchingState : BaseState
    {

        public CrouchingState(Character player) : base(player)
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