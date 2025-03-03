using System.Collections.Generic;
using UnityEngine;

public class PathHolder : MonoBehaviour
{
    [SerializeField] private List<Transform> _paths;

    public IReadOnlyList<Transform> Paths => _paths;
}
