using Godot;
namespace CharacterState
{
    public abstract class AbstractState
    {
        public CharacterStateManager sharedState;

        public AbstractState(CharacterStateManager csm)
        {
            this.sharedState = csm;
        }
        public abstract AbstractState HandleEvent(InputEvent ev);

        public abstract AbstractState PhysicsProcess(float dt);
    }
}
