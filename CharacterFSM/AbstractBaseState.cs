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
        public abstract AbstractState HandleEvent(InputEvent ev);

        public abstract AbstractState PhysicsProcess(float dt);
    }
}
