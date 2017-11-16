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