using Godot;
namespace CharacterState
{
    public class RunningState : BaseState
    {

        public RunningState(Character player) : base(player)
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