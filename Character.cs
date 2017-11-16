using Godot;
using CharacterState;

public class Character : KinematicBody {
	
	public Camera viewCamera = new Camera();
	public Vector3 movementVector = new Vector3();
	public float verticalVelocity = 0;


	private AbstractState state;

	public override void _Ready() {
		Input.SetMouseMode(Input.MOUSE_MODE_CAPTURED);
		state = new StandingState(this);
		AddChild(viewCamera);
	}

	public override void _Input(InputEvent ev) {
		state = state.handleEvent(ev);
	}

	public override void _PhysicsProcess(float dt) {
		state = state.physicsProcess(dt);
	}
}