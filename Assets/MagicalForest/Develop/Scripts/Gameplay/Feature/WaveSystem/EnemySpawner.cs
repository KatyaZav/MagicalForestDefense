using Assets.Gameplay.Features.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gameplay.Features.Wave
{
    public class EnemySpawner : IDisposable
    {
        public event Action EnemiesGone;

        private Transform _spawnPosition;
        private Transform _folder;

        private List<BaseEnemy> _enemiesList = new List<BaseEnemy>();

        public EnemySpawner(Transform spawnPosition, Transform folder) 
        {
            _spawnPosition = spawnPosition;
            _folder = folder;
        }

        private IReadOnlyList<BaseEnemy> Enemies => _enemiesList;

        public void Dispose()
        {
            ClearList();
        }

        public void SpawnEnemy(BaseEnemy enemy)
        {
            var newEnemy = 
                GameObject.Instantiate(enemy, _spawnPosition.position, Quaternion.identity, _folder);

            newEnemy.Died += OnEnemyDied;

            _enemiesList.Add(newEnemy);
        }

        private void ClearList()
        {
            foreach (var enemy in _enemiesList)
            {
                enemy.Died -= OnEnemyDied;
                GameObject.Destroy(enemy.gameObject);
            }
        }

        private void OnEnemyDied(BaseEnemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            _enemiesList.Remove(enemy);

            if (_enemiesList.Count == 0)
                EnemiesGone?.Invoke();
        }
    }
}
