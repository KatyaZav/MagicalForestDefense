using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gameplay.Features.Wave.Configs
{
    [System.Serializable]
    public class WaveConfig
    {
        [SerializeField] private List<WaveEnemy> _waveEnemies;        
        
        [field: SerializeField] public bool IsBoss { get; private set; } = false;
        public IReadOnlyList<WaveEnemy> WaveEnemies => _waveEnemies;
    }
}
