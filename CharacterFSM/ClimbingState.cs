using Godot;
namespace CharacterState {
    public class ClimbingState : AbstractState {

        public ClimbingState(Character player) : base(player) {
        }

        public override AbstractState handleEvent(InputEvent ev) {
            return null;
        }

        public override AbstractState physicsProcess(float dt) {
            return null;
        }
    }
}