using Godot;
namespace CharacterState
{
    public abstract class AbstractState
    {
        public Vector3 up = new Vector3(0, 1, 0);

        public const float mouseSensitivity = 0.005f;
        public const float gravity = 9.8f;
        public const float jumpHeight = 5f;
        public const float runMultiplier = 3f;
        public const float walkSpeed = 100f;

        public Character player;

        public AbstractState(Character player)
        {
            this.player = player;
        }
        public virtual AbstractState handleEvent(InputEvent ev)
        {
            if (ev is InputEventMouseMotion)
            {
                handleMouse((InputEventMouseMotion)ev);
            }
            return null;
        }

        public virtual AbstractState physicsProcess(float dt)
        {

            return null;
        }

        public void handleMouse(InputEventMouseMotion iemm)
        {
            Vector2 motion = -iemm.GetRelative();
            player.RotateY(motion.x * mouseSensitivity);

            float xrot = Mathf.max(Mathf.min(motion.y * mouseSensitivity + player.viewCamera.GetRotation().x, Mathf.PI / 2), -Mathf.PI / 2);
            player.viewCamera.SetRotation(new Vector3(xrot, 0, 0));
        }
    }
}
