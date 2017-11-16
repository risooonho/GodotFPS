using Godot;
using CharacterState;

public class Character : KinematicBody {
	
	public Camera viewCamera = new Camera();
	public Vector3 movementVector = new Vector3();
	public float verticalVelocity = 0;


	private BaseState currentState;

	public override void _Ready() {
		Input.SetMouseMode(Input.MOUSE_MODE_CAPTURED);
		currentState = new StandingState(this);
		AddChild(viewCamera);
	}

	public override void _Input(InputEvent ev) {
		BaseState update = currentState.handleEvent(ev);
		if (update == null)
		{
			return;
		}
		currentState = update;
	}

	public override void _PhysicsProcess(float dt) {
		BaseState update = currentState.physicsProcess(dt);
		if (update == null)
		{
			return;
		}
		currentState = update;
	}
}