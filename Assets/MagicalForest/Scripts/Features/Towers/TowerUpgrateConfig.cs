using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gameplay.Features.Towers
{
    [CreateAssetMenu(menuName
        = "Configs/Gameplay/Tower/NewTowerUpgrateConfig", fileName = "TowerUpgrateConfig")]
    public class TowerUpgrateConfig : ScriptableObject
    {
        [SerializeField] private List<UpgrateValue> _values;

        public IReadOnlyList<UpgrateValue> ValuesList => _values;
    }

    [System.Serializable]
    public class UpgrateValue
    {
        [field: SerializeField] public float Cost { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}
