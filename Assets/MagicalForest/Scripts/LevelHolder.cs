using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private PathHolder _pathHolder;

    public IReadOnlyList<Transform> Path => _pathHolder.Paths;

    public Queue<Transform> GetQueue()
    {
        Queue<Transform> queue = new Queue<Transform>();

        foreach (Transform t in _pathHolder.Paths)
        {
            queue.Enqueue(t);
        }

        return queue;
    }
}
