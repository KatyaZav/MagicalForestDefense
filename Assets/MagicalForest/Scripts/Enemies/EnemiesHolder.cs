using System.Collections.Generic;

public class EnemiesHolder
{
    private List<Enemy> _enemies = new List<Enemy>();

    public IReadOnlyList<Enemy> Enemies => _enemies;

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (_enemies.Remove(enemy) == false)
            throw new System.Exception("Try to remove enemy not exist");
    }
}
