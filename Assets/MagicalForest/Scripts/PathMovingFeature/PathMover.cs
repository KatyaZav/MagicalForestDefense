using System;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : IUpdatable
{
    private const float MinimumDistance = 0.01f;

    public event Action PathCompleted;

    private Mover _mover;
    private Queue<Vector3> _paths;
    private Vector3 _currentTarget;

    public PathMover(Mover mover, Queue<Vector3> paths)
    {
        _mover = mover;
        _paths = paths;

        _currentTarget = _paths.Dequeue();
        _mover.MoveTo(_currentTarget);
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
            _mover.MoveTo(_currentTarget);
        }
    }

    private bool CheakIsCompletePath()
    {
        return Mathf.Abs(_mover.CurrentPosition.x - _currentTarget.x) < MinimumDistance
            && Mathf.Abs(_mover.CurrentPosition.y - _currentTarget.y) < MinimumDistance;
    }
}
