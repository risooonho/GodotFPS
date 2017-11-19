using Godot;

using CharacterState;
using CharacterState.Movement;

public class Character : KinematicBody {
	
	private const float maximumClimbableSlope = Mathf.PI / 4;
	private Vector3 up = new Vector3(0,1,0);

	public Vector3 movementVector = new Vector3();
	public Vector3 otherForces = new Vector3();

	public Camera viewCamera;
	public CapsuleShape moveShape;
	public Tween heightTween;

	private AbstractState state;
	
	public bool crouchHeld = false;
	public bool runHeld = false;

	public override void _Ready() {
		moveShape = (CapsuleShape)((CollisionShape)GetNode("moveShape")).GetShape();
		viewCamera = (Camera) GetNode("Camera");
		heightTween = (Tween) GetNode("HeightTween");

		Input.SetMouseMode(Input.MOUSE_MODE_CAPTURED);
		state = new StandingState(this);
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
			heightTween.InterpolateProperty(moveShape, "height", moveShape.GetHeight(), height, 0.25f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);
			heightTween.InterpolateProperty(viewCamera, "translation", viewCamera.GetTranslation(), new Vector3 { y = height / 2 }, 0.25f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);
			//tween.InterpolateProperty(this, "translation", this.GetTranslation(), this.GetTranslation() - new Vector3 { y = (heightDifference) / 2 }, 0.5f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);


			//moveShape.SetHeight(height);
			//viewCamera.SetTranslation(new Vector3 { y = height / 2 });
			//this.Translate(new Vector3 { y = (heightDifference) / 2 });
			heightTween.Start();

		}

	}

	/**
	 * Where the player wants to consciously go.
	 * If I get pushed forward by someone, that isn't a conscious desire to go in that direction.
	 * If I decide to walk over and pick something up, that is conscious movement.
	 **/
	public Vector3 consciousMovement(Vector3 movement)
	{
		return MoveAndSlide(movement, up, 0.05f, 4, maximumClimbableSlope);
	}

	/**
	 * Stuff like gravity, things out of direct player control
	 **/
	public KinematicCollision unconsciousMovement(Vector3 movement)
	{
		return MoveAndCollide(movement);
	}
}