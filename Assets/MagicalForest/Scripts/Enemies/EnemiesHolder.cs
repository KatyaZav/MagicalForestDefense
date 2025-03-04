using System;
using System.Collections.Generic;

public class EnemiesHolder
{
    public event Action<int> ListChanged;
    private List<Enemy> _enemies = new List<Enemy>();

    public IReadOnlyList<Enemy> Enemies => _enemies;

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        ListChanged?.Invoke(_enemies.Count);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (_enemies.Remove(enemy) == false)
            throw new System.Exception("Try to remove enemy not exist");
        
        ListChanged?.Invoke(_enemies.Count);
    }
}
