﻿using Assets.Servises.Configs;
using UnityEngine;

namespace Assets.Gameplay.Features.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Configs/Gameplay/Enemy/EnemyData", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        [field:SerializeField] public TextTranslateConfig Name {  get; private set; }
        [field: SerializeField] public int Reward { get; private set; } = 10;
        [field:SerializeField] public int Speed { get; private set; }
        [field:SerializeField] public int Health { get; private set; }
        [field:SerializeField] public GameObject Model { get; private set; }
    }
}
