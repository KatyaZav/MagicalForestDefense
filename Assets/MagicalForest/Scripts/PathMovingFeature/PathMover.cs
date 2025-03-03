using System;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : IUpdatable
{
    private const float MinimumDistance = 0.01f;

    public event Action PathCompleted;

    private Mover _mover;
    private Queue<Transform> _paths;
    private Transform _currentTarget;

    public PathMover(Mover mover, Queue<Transform> paths)
    {
        _mover = mover;
        _paths = paths;

        _currentTarget = _paths.Dequeue();
        _mover.MoveTo(_currentTarget.position);
    }

    public void CustomUpdate(float deltaTime)
    {
        _mover.CustomUpdate(deltaTime);

        if (CheakIsCompletePath())
        {
            if (_paths.Count == 0)
            {
                PathCompleted?.Invoke();
                return;
            }

            _currentTarget = _paths.Dequeue();
        }
    }

    private bool CheakIsCompletePath()
    {
        return Mathf.Abs(_mover.CurrentPosition.x - _currentTarget.position.x) < MinimumDistance
            && Mathf.Abs(_mover.CurrentPosition.y - _currentTarget.position.y) < MinimumDistance;
    }
}
