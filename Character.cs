using Godot;

public class Character : KinematicBody {

	public const float mouselookSensitivity = 0.005f;
	public const float gravity = 9.8f;
	public const float jumpHeight = 5f;
	public const float runMultiplier = 3f;
	public const float walkSpeed = 100f;

	private Camera characterCamera = new Camera();
	private Vector3 movementVector = new Vector3();

	private enum STATE {
		STANDING,
		WALKING,
		RUNNING,
		FALLING,
		CROUCHING,
		CROUCHWALKING,
		CLIMBING
	}

	private STATE currentState = STATE.STANDING;
	private float verticalVelocity = 0;

	public override void _Ready() {
		Input.SetMouseMode(Input.MOUSE_MODE_CAPTURED);

		AddChild(characterCamera);
	}

	public override void _Input(InputEvent ev) {
		if (ev is InputEventMouseMotion) {
			Vector2 motion = -((InputEventMouseMotion) ev).GetRelative();
			RotateY(motion.x * mouselookSensitivity);

			float xrot = Mathf.max(Mathf.min(motion.y * mouselookSensitivity + characterCamera.GetRotation().x, Mathf.PI / 2), -Mathf.PI / 2);
			characterCamera.SetRotation(new Vector3(xrot,0,0));
		}
		else {
			//Clamp axis to prevent weird movement
			movementVector.z = Mathf.min(1, Mathf.max(-1, movementVector.z));
			movementVector.x = Mathf.min(1, Mathf.max(-1, movementVector.x));

			if (ev.IsActionPressed("character_forward") || ev.IsActionReleased("character_backward")) {
				movementVector.z--; 
			}
			else if (ev.IsActionReleased("character_forward") || ev.IsActionPressed("character_backward")) {
				movementVector.z++;
			}
			else if (ev.IsActionPressed("character_strafe_right") || ev.IsActionReleased("character_strafe_left")) {
				movementVector.x++;
			}
			else if (ev.IsActionReleased("character_strafe_right") || ev.IsActionPressed("character_strafe_left")) {
				movementVector.x--;
			}
			else if (ev.IsActionPressed("character_jump")) {
				bool inJumpableState = 
					currentState == STATE.STANDING || 
					currentState == STATE.WALKING || 
					currentState == STATE.RUNNING;

				if (inJumpableState) {
					verticalVelocity = jumpHeight;
				}
			}
			else if (ev.IsActionPressed("character_run")) {
				bool canRun = 
					currentState == STATE.WALKING ||
					currentState == STATE.STANDING;
				
				if (canRun) {
					currentState = STATE.RUNNING;
				}
			}
			else if (ev.IsActionReleased("character_run")) {
				bool changeToWalk = 
					currentState == STATE.RUNNING;

				if (changeToWalk) {
					currentState = STATE.WALKING;
				}
			}
		}
	}

	public override void _PhysicsProcess(float dt) {
		verticalVelocity -= gravity * dt;
		Vector3 computedGravity = new Vector3(0, (verticalVelocity) * dt, 0);
		Vector3 computedDirection = (movementVector.rotated(new Vector3(0, 1, 0), this.GetRotation().y)).normalized() * walkSpeed * dt;

		if (currentState == STATE.RUNNING) {
			computedDirection *= runMultiplier;
		}

		KinematicCollision kc = MoveAndCollide(computedGravity);

		if (kc == null) {	//Character not colliding with anything (includes floor)
			bool fallableState = 
				currentState == STATE.STANDING ||
				currentState == STATE.WALKING ||
				currentState == STATE.RUNNING ||
				currentState == STATE.CROUCHING;
			
			if (fallableState) {
				currentState = STATE.FALLING;
			}
		} 
		else {	//Character colliding with something
			if (kc.GetColliderId() != 0 && kc.GetNormal().y > 0) {	//Collision must be with object below character
				verticalVelocity = 0;

				bool transitionFromFallingState =
					currentState == STATE.FALLING;

				if (transitionFromFallingState) {
					currentState = STATE.STANDING;
				}
			}
		}
		MoveAndSlide(computedDirection, new Vector3(0,1,0), 1f, 4, Mathf.PI/4);
	}
}