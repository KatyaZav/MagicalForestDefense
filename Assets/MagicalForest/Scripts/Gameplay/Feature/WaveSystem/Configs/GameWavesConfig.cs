using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gameplay.Features.Wave.Configs
{
    [CreateAssetMenu(fileName = "WavesData", menuName = "Configs/Gameplay/WaveConfigs/GameWaves", order = 0)]
    public class GameWavesConfig : ScriptableObject
    {
        [SerializeField] private List<WaveConfig> _wavesConfig = new List<WaveConfig>();

        private IReadOnlyList<WaveConfig> GameWaves => _wavesConfig;
    }
}
