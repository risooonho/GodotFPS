using Godot;

using CharacterState;
using CharacterState.MovementFocused;

public class Character : KinematicBody {
	
	public Camera viewCamera = new Camera();
	public Vector3 movementVector = new Vector3();
	public Vector3 otherForces = new Vector3();

	public CapsuleShape moveShape;
	private AbstractState state;

	public bool crouchHeld = false;
	public bool runHeld = false;

	public override void _Ready() {
		moveShape = (CapsuleShape)((CollisionShape)GetNode("moveShape")).GetShape();
		Input.SetMouseMode(Input.MOUSE_MODE_CAPTURED);
		state = new StandingState(this);
		AddChild(viewCamera);
	}

	public override void _Input(InputEvent ev) {
		state = state.HandleEvent(ev);
	}

	public override void _PhysicsProcess(float dt) {
		state = state.PhysicsProcess(dt);
	}

	public void ChangeHeight(float height)
	{
		float heightDifference = height - moveShape.GetHeight();
		if (heightDifference != 0)
		{
			moveShape.SetHeight(height);
			viewCamera.SetTranslation(new Vector3 { y = height / 2 });
			this.Translate(new Vector3 { y = (heightDifference) / 2 });
		}

	}
}