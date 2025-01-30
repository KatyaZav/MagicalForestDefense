using UnityEngine;

namespace Assets.Gameplay.Features.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Configs/Gameplay/Enemy/EnemyData", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        [field:SerializeField] public string Name {  get; private set; }
        [field:SerializeField] public int Speed { get; private set; }
        [field:SerializeField] public int Health { get; private set; }
        [field:SerializeField] public GameObject Model { get; private set; }
    }
}
