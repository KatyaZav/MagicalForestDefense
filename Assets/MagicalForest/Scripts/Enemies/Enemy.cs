using Assets.Gameplay.Features.EnemyData;
using DI.Game.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IUpdatable
{
    public event Action<Enemy> Died, PathCompleted;

    private PathMover _pathMover;
    private HealthBehaviour _health;

    public void Init(EnemyConfig config, Queue<Vector3> path)
    {
        Mover mover = new Mover(transform, config.Speed);

        _pathMover = new PathMover(mover, path);
        _health = new HealthBehaviour(new ReactiveVariable<float>(config.Health));

        _pathMover.PathCompleted += OnPathCompleted;
    }

    public IReadOnlyVariable<float> Health => _health.Health;

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    public void CustomUpdate(float deltaTime)
    {
        if (_health._isDied)
            return;

        _pathMover?.CustomUpdate(deltaTime);
    }

    private void OnPathCompleted()
    {
        PathCompleted?.Invoke(this);
    }
}
