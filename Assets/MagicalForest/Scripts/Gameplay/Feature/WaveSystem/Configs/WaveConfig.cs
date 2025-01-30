using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gameplay.Features.Wave.Configs
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Configs/Gameplay/WaveConfigs/WaveData", order = 1)]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private List<WaveEnemy> _waveEnemies;        
        
        [field: SerializeField] public bool IsBoss { get; private set; } = false;
        public IReadOnlyList<WaveEnemy> WaveEnemies => _waveEnemies;
    }
}
