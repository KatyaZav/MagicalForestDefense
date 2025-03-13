using System;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : IUpdatable
{
    private const float MinimumDistance = 0.1f;

    public event Action PathCompleted;

    private Mover _mover;
    private Queue<Vector3> _paths;

    public PathMover(Mover mover, Queue<Vector3> paths)
    {
        _mover = mover;
        _paths = paths;

        CurrentTarget = _paths.Dequeue();
        _mover.MoveTo(CurrentTarget);
    }

    public Vector3 CurrentTarget { get; private set; }

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

            CurrentTarget = _paths.Dequeue();
            _mover.MoveTo(CurrentTarget);
        }
    }

    private bool CheakIsCompletePath()
    { 
        return Mathf.Abs(_mover.CurrentPosition.x - CurrentTarget.x) < MinimumDistance
            && Mathf.Abs(_mover.CurrentPosition.z - CurrentTarget.z) < MinimumDistance;
    }
}
