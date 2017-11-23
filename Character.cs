using Godot;
using CharacterState;

public class Character : KinematicBody {

	private const float maximumClimbableSlope = Mathf.PI / 4;
	private Vector3 up = new Vector3(0,1,0);

	private Camera camera;
	private Spatial cameraSpatial;

	private CapsuleShape moveShape;

	private Tween heightTween;
	private Tween leanTween;
	private Tween bobbingTween;

	private CharacterStateManager stateManager;

	public override void _Ready()
	{
		camera = (Camera)GetNode("CameraSpatial/Camera");
		cameraSpatial = (Spatial)GetNode("CameraSpatial");

		moveShape = (CapsuleShape)((CollisionShape)GetNode("moveShape")).GetShape();

		heightTween = (Tween) GetNode("CameraSpatial/HeightTween");
		leanTween = (Tween)GetNode("CameraSpatial/LeanTween");
		bobbingTween = (Tween)GetNode("CameraSpatial/Camera/BobbingTween");

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
			Vector3 cameraHeightTranslation = camera.GetTranslation();
			cameraHeightTranslation.y = height / 2;
			heightTween.InterpolateProperty(camera, "translation", camera.GetTranslation(), cameraHeightTranslation, 0.1f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);
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
		Vector3 nextCameraRotation = new Vector3
		{
			x = Mathf.max(Mathf.min(rotation.x + camera.GetRotation().x, Mathf.PI / 2), -Mathf.PI / 2) //Clamp vertical camera movement
		};
		camera.SetRotation(nextCameraRotation);

		Vector3 nextCameraSpatialRotation = rotation + cameraSpatial.GetRotation();
		nextCameraSpatialRotation.x = 0;
		cameraSpatial.SetRotation(nextCameraSpatialRotation);

	}

	public Vector3 GetCharacterRotation()
	{
		return cameraSpatial.GetRotation();
	}

	public Vector3 GetCameraRotation()
	{
		return new Vector3
		{
			x = camera.GetRotation().x,
			y = cameraSpatial.GetRotation().y,
			z = cameraSpatial.GetRotation().z
		};
	}

	public void LeanAtDegrees(float angle)
	{
		Vector3 tempRotationDeg = cameraSpatial.GetRotationDeg();

		Vector3 futureRotation = tempRotationDeg;
		futureRotation.z = angle;
		leanTween.InterpolateProperty(cameraSpatial, "rotation_deg", tempRotationDeg, futureRotation, 0.1f, Tween.TRANS_LINEAR, Tween.EASE_IN_OUT);

		leanTween.Start();
	}
}