using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gameplay.Features.Wave.Configs
{
    [System.Serializable]
    public class WaveConfig
    {
        [SerializeField] private List<WaveEnemy> _waveEnemies;        
        
        [field: SerializeField] public bool IsBoss { get; private set; } = false;
        [field: SerializeField] public int WaitTime { get; private set; } = 20;
        public IReadOnlyList<WaveEnemy> WaveEnemies => _waveEnemies;
    }
}
