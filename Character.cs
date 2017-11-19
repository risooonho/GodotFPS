using Godot;
using CharacterState;

public class Character : KinematicBody {

	private const float maximumClimbableSlope = Mathf.PI / 4;
	private Vector3 up = new Vector3(0,1,0);

	private Camera viewCamera;
	private CapsuleShape moveShape;
	private Tween heightTween;

	private CharacterStateManager stateManager;

	public override void _Ready()
	{
		viewCamera = (Camera)GetNode("Camera");
		moveShape = (CapsuleShape)((CollisionShape)GetNode("moveShape")).GetShape();

		heightTween = (Tween) GetNode("HeightTween");

		Input.SetMouseMode(Input.MOUSE_MODE_CAPTURED);
		stateManager = new CharacterStateManager(this);
	}

	public override void _Input(InputEvent ev) {
		stateManager._Input(ev);
	}

	public override void _PhysicsProcess(float dt) {
		stateManager._PhysicsProcess(dt);
	}

	public void ChangeHeight(float height)
	{
		//Make sure that there is a difference between current height and requested height
		if (height - moveShape.GetHeight() != 0)
		{
			heightTween.InterpolateProperty(moveShape, "height", moveShape.GetHeight(), height, 0.1f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);
			heightTween.InterpolateProperty(viewCamera, "translation", viewCamera.GetTranslation(), new Vector3 { y = height / 2 }, 0.1f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);
			heightTween.Start();
		}
	}

	/**
	 * Where the player wants to consciously go.
	 * If I get pushed forward by someone, that isn't a conscious desire to go in that direction.
	 * If I decide to walk over and pick something up, that is conscious movement.
	 **/
	public Vector3 ConsciousMovement(Vector3 movement)
	{
		return MoveAndSlide(movement, up, 1f, 4, maximumClimbableSlope);
	}

	/**
	 * Stuff like gravity, things out of direct player control
	 **/
	public KinematicCollision UnconsciousMovement(Vector3 movement)
	{
		return MoveAndCollide(movement);
	}

	public void RotateCamera(Vector3 rotation)
	{
		Vector3 computedRotation = rotation + viewCamera.GetRotation();
		computedRotation.x = Mathf.max(Mathf.min(computedRotation.x, Mathf.PI / 2), -Mathf.PI / 2); //Clamp vertical axis
		viewCamera.SetRotation(computedRotation);
	}

	public Vector3 GetCharacterRotation()
	{
		return viewCamera.GetRotation();
	}

    public bool NoStandingSpace()
    {
        return IsOnCeiling();
    }
}