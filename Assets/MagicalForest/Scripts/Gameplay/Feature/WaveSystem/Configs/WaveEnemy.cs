using Assets.Gameplay.Features.Enemy;
using UnityEngine;

namespace Assets.Gameplay.Features.Wave.Configs
{
    [System.Serializable]
    public class WaveEnemy
    {
        [field: SerializeField] public EnemyConfig Enemy { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}
