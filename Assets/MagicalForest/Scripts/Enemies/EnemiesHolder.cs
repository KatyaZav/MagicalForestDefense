using UnityEngine;

public class EnemiesHolder : IUpdatable
{
    private EnemiesListHolderSystem _enemiesListHolderSystem;
    private PlayerGameplaySaves _saves;

    public EnemiesHolder(EnemiesListHolderSystem enemiesListHolderSystem, PlayerGameplaySaves saves)
    {
        _enemiesListHolderSystem = enemiesListHolderSystem;
        _saves = saves;

        _enemiesListHolderSystem.Added += OnAdded;
        _enemiesListHolderSystem.Removed += OnRemoved;
    }

    private void OnAdded(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
        enemy.PathCompleted += OnPathCompleted;
    }

    private void OnRemoved(Enemy enemy)
    {
        enemy.Dispose();

        enemy.Died -= OnEnemyDied;
        enemy.PathCompleted -= OnPathCompleted;
    }

    public void CustomUpdate(float deltaTime)
    {
        for (int i = 0; i < _enemiesListHolderSystem.Enemies.Count; i++)
        {
            var enemy = _enemiesListHolderSystem.Enemies[i];
            enemy?.CustomUpdate(deltaTime);
        }
    }
    private void OnPathCompleted(Enemy enemy)
    {
        _saves.RemoveHealth(enemy.Damage);

        _enemiesListHolderSystem.RemoveEnemy(enemy);
        GameObject.Destroy(enemy.gameObject);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _saves.AddCoins(enemy.Reward);

        _enemiesListHolderSystem.RemoveEnemy(enemy);
        GameObject.Destroy(enemy.gameObject);
    }
}
