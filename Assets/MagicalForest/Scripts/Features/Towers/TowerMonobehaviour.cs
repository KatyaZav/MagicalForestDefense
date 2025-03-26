using UnityEngine;

namespace Assets.Gameplay.Features.Towers
{
    public class TowerMonobehaviour : MonoBehaviour, IUpdatable
    {
        [SerializeField] private LayerMask _enemyLayer;
        
        [SerializeField] private TowerUpgrateConfig _attackSpeed;
        [SerializeField] private TowerUpgrateConfig _attackRange;
        [SerializeField] private TowerUpgrateConfig _attackDamage;

        private int _attackLevel, _rangeLevel, _damageLevel = 0;

        private ITowerDamage _towerDamage;
        private Collider[] _hitsCache = new Collider[32];

        public TowerMonobehaviour(LayerMask enemyLayer,
            TowerUpgrateConfig attackSpeed, TowerUpgrateConfig attackRange,
            TowerUpgrateConfig attackDamage, ITowerDamage towerDamage)
        {
            _enemyLayer = enemyLayer;
            _attackSpeed = attackSpeed;
            _attackRange = attackRange;
            _attackDamage = attackDamage;

            _towerDamage = towerDamage;
        }

        public bool CanAddSpeed => _attackSpeed.ValuesList.Count > _attackLevel;
        public bool CanAddRange => _attackRange.ValuesList.Count > _rangeLevel;
        public bool CanAddDamage => _attackDamage.ValuesList.Count > _damageLevel;

        public void CustomUpdate(float deltaTime)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(
               transform.position,
               _attackRange.ValuesList[_rangeLevel].Value,
               _hitsCache,
               _enemyLayer
           );

            _towerDamage.Attack(_hitsCache);
        }

        public void UpdateSpeed()
        {
            if (CanAddSpeed == false)
                throw new System.Exception("Can't add more speed");

            _attackLevel++;
        }

        public void UpdateRange()
        {
            if (CanAddRange == false)
                throw new System.Exception("Can't add more range");

            _rangeLevel++;
        }

        public void UpdateDamage()
        {
            if (CanAddDamage == false)
                throw new System.Exception("Can't add more damage");

            _damageLevel++;
        }
    }
}
