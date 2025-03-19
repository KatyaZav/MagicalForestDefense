using UnityEngine;

public class EnemiesHolder : IUpdatable
{
    private EnemiesListHolderSystem _enemiesListHolderSystem;

    public EnemiesHolder(EnemiesListHolderSystem enemiesListHolderSystem)
    {
        _enemiesListHolderSystem = enemiesListHolderSystem;

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
        _enemiesListHolderSystem.RemoveEnemy(enemy);
        GameObject.Destroy(enemy.gameObject);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        Debug.Log("damage");

        _enemiesListHolderSystem.RemoveEnemy(enemy);
        GameObject.Destroy(enemy.gameObject);
    }

}
