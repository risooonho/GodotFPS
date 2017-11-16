using Godot;
namespace CharacterState
{
    public class CrouchWalkingState : BaseState
    {

        public CrouchWalkingState(Character player) : base(player)
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