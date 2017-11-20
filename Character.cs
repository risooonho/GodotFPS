using Godot;
using CharacterState;

public class Character : KinematicBody {

	private const float maximumClimbableSlope = Mathf.PI / 4;
	private Vector3 up = new Vector3(0,1,0);

	private Camera viewCamera;
	private CapsuleShape moveShape;

	private Tween heightTween;
	private Tween leanTween;

	private CharacterStateManager stateManager;

	public override void _Ready()
	{
		viewCamera = (Camera)GetNode("Camera");
		moveShape = (CapsuleShape)((CollisionShape)GetNode("moveShape")).GetShape();

		heightTween = (Tween) GetNode("HeightTween");
		leanTween = (Tween)GetNode("LeanTween");

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
			Vector3 cameraHeightTranslation = viewCamera.GetTranslation();
			cameraHeightTranslation.y = height / 2;
			heightTween.InterpolateProperty(viewCamera, "translation", viewCamera.GetTranslation(), cameraHeightTranslation, 0.1f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);
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

	public void LeanAtDegrees(float angle)
	{
		Vector3 tempRotationDeg = viewCamera.GetRotationDeg();

		Vector3 futureRotation = tempRotationDeg;
		futureRotation.z = angle;
		leanTween.InterpolateProperty(viewCamera, "rotation_deg", tempRotationDeg, futureRotation, 0.1f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);

		//This here is real hacky, will fix when I finalize how character rotation works
		Vector3 axis = new Vector3 { x = Mathf.cos(Mathf.deg2rad(tempRotationDeg.y)), z = Mathf.sin(Mathf.deg2rad(tempRotationDeg.y)) };
		GD.print("Axis: " + axis);

		Vector3 futureTranslation = viewCamera.GetTranslation();
		float rotationDelta = Mathf.deg2rad(angle - tempRotationDeg.z);
		GD.print("RotationDelta: " + rotationDelta);
		GD.print("Prev: " + futureTranslation);
		futureTranslation = futureTranslation.rotated(axis, rotationDelta);
		GD.print("Future: " + futureTranslation);

		leanTween.InterpolateProperty(viewCamera, "translation", viewCamera.GetTranslation(), futureTranslation, 0.1f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);

		leanTween.Start();
	}

	public bool NoStandingSpace()
	{
		return IsOnCeiling();
	}
}