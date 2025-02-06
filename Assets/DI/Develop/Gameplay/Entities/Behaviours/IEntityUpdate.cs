namespace DI.Game.Develop.Gameplay.Entities.Behaviours
{
    public interface IEntityUpdate: IEntityBehaviour
    {
        void OnUpdate(float deltaTime);
    }
}
