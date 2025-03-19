using Assets.Gameplay.Features.EnemyData;
using DI.Game.Develop.Utils.Reactive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IUpdatable
{
    public event Action<Enemy> Died, PathCompleted;

    private PathMover _pathMover;
    private ReactiveVariable<float> _health;

    private bool _isDead;
    
    public void Init(EnemyConfig config, Queue<Vector3> path)
    {
        Mover mover = new Mover(transform, config.Speed);
        _pathMover = new PathMover(mover, path);

        _pathMover.PathCompleted += OnPathCompleted;
    }

    public IReadOnlyVariable<float> Health => _health;

    public void TakeDamage(float damage)
    {
        _health.Value -= damage;

        if (_health.Value <= 0)
        {
            OnDead();
        }
    }

    public void CustomUpdate(float deltaTime)
    {
        if (_isDead)
            return;

        _pathMover?.CustomUpdate(deltaTime);
    }

    private void OnDead()
    {
        _isDead = true;
        Died?.Invoke(this);
    }

    private void OnPathCompleted()
    {
        PathCompleted?.Invoke(this);
    }

}
