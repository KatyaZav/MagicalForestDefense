public class EnemiesHolder : IUpdatable
{
    private EnemiesListHolderSystem _enemiesListHolderSystem;

    public EnemiesHolder(EnemiesListHolderSystem enemiesListHolderSystem)
    {
        _enemiesListHolderSystem = enemiesListHolderSystem;
    }

    public void CustomUpdate(float deltaTime)
    {
        foreach (var enemy in _enemiesListHolderSystem.Enemies)
        {
            enemy.CustomUpdate(deltaTime);
        }
    }
}
