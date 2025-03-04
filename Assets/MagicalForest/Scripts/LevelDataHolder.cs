using System.Collections.Generic;
using UnityEngine;

public class LevelDataHolder : MonoBehaviour
{
    [SerializeField] private PathHolder _pathHolder;

    public IReadOnlyList<Transform> Path => _pathHolder.Paths;

    public Queue<Vector3> GetQueue()
    {
        Queue<Vector3> queue = new Queue<Vector3>();

        foreach (Transform t in _pathHolder.Paths)
        {
            Vector3 newVector = t.position;
            queue.Enqueue(newVector);
        }

        return queue;
    }
}
