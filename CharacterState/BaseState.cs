using Godot;

public abstract class BaseState {
    public abstract BaseState handleEvent(InputEvent ev);
    public abstract void character_move();
}