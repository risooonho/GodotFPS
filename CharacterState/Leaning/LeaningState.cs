using Godot;

namespace CharacterState.Leaning
{
    public class LeaningState : AbstractLeaningState
    {
        public LeaningState(CharacterStateManager sharedState, float angle) : base(sharedState) 
        {
            this.angle = angle;
            sharedState.LeanAtDegrees(angle);
        }

        public override AbstractState HandleEvent(InputEvent ev)
        {
            
            if ((angle > 0 && ev.IsActionReleased("character_lean_left"))
                || (angle < 0 && ev.IsActionReleased("character_lean_right")))
            {
                return new NoLeaningState(sharedState);
            }
            return this;
        }
    }
}
