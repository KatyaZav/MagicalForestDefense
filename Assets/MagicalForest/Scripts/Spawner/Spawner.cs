using Assets.Gameplay.Features.EnemyData;
using DI.Game.Develop.CommonServices.CoroutinePerfomer;
using DI.Game.Develop.Utils.Reactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    private const float TimeBetween = 0.1f;

    private ICoroutinePerformer _coroutinePerformer;

    private EnemiesHolder _holder;
    private Queue<Transform> _path;
    private Transform _startPoint;

    private ReactiveVariable<bool> _isSpawning = new ReactiveVariable<bool>(false);

    public Spawner(EnemiesHolder holder, Queue<Transform> path, ICoroutinePerformer root)
    {
        _holder = holder;
        _path = path;
        _coroutinePerformer = root;

        _startPoint = _path.Peek();
    }

    public IReadOnlyVariable<bool> IsSpawning => _isSpawning;

    public void SpawnEnemies(EnemyConfig config, float count)
    {
        if (_isSpawning.Value == true)
            throw new System.Exception("Tried to start spawn, but current spawn not complete!");

        _coroutinePerformer.StartPerform(SpawnerEnemies(config, count));
    }

    private IEnumerator SpawnerEnemies(EnemyConfig config, float count)
    {
        _isSpawning.Value = true;

        for (var i = 0; i < count; i++)
        {
            SpawnEnemy(config);
            yield return new WaitForSeconds(TimeBetween);
        }

        _isSpawning.Value = false;
    }

    private void SpawnEnemy(EnemyConfig enemyConfig)
    {
        if (enemyConfig.Model == null)
            throw new System.Exception("Model is null");

        Enemy spawnerEnemy = GameObject.Instantiate(enemyConfig.Model,
            _startPoint.position, _startPoint.rotation);

        spawnerEnemy.Init(enemyConfig, new Queue<Transform>(_path));

        _holder.AddEnemy(spawnerEnemy);
    }
}
