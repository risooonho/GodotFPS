using Godot;

namespace CharacterState.Leaning
{
    public class NoLeaningState : AbstractLeaningState
    {
        public NoLeaningState(CharacterStateManager csm) : base(csm)
        {
            sharedState.LeanAtDegrees(0);
        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            if (ev.IsActionPressed("character_lean_left"))
            {
                return new LeaningState(sharedState, leanAngle);
            }
            else if (ev.IsActionPressed("character_lean_right"))
            {
                return new LeaningState(sharedState, -leanAngle);
            }
            return this;
        }
    }
}