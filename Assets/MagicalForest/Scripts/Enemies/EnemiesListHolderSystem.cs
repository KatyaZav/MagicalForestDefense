using System;
using System.Collections.Generic;

public class EnemiesListHolderSystem
{
    public event Action RemovedLast;

    public event Action<Enemy> Added, Removed;
    private List<Enemy> _enemies = new List<Enemy>();

    public IReadOnlyList<Enemy> Enemies => _enemies;

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        Added?.Invoke(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (_enemies.Remove(enemy) == false)
            throw new System.Exception("Try to remove enemy not exist");
        
        Removed?.Invoke(enemy);

        if (_enemies.Count == 0)
            RemovedLast?.Invoke();
    }
}
