using Godot;
namespace CharacterState
{
    public abstract class BaseState
    {
        public const float mouseSensitivity = 0.005f;
        public const float gravity = 9.8f;
        public const float jumpHeight = 5f;
        public const float runMultiplier = 3f;
        public const float walkSpeed = 100f;

        public Character player;

        public BaseState(Character player)
        {
            this.player = player;
        }
        public virtual BaseState handleEvent(InputEvent ev)
        {
            if (ev is InputEventMouseMotion)
            {
                handleMouse((InputEventMouseMotion)ev);
            }
            return null;
        }

        public virtual BaseState physicsProcess(float dt)
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
