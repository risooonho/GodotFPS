using Godot;

public class ClimbingState : BaseState {

    public override BaseState handleEvent(InputEvent ev) {
        return new ClimbingState();
    }

    public override void character_move() {
        return;
    }
}