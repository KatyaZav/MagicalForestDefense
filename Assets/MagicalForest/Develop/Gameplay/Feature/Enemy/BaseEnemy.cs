using Assets.Servises.Interfaces;
using System;
using UnityEngine;

namespace Assets.Gameplay.Features.Enemy
{
    public class BaseEnemy : MonoBehaviour, IUpdatable
    {
        public event Action<BaseEnemy> Died;

        private float _health;
        private bool _isDead = false;

        private Vector3 _direction;

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void TakeDamage(float damage)
        {
            if (_isDead == false)
                throw new ApplicationException("Try to remove health from dead enemy");

            _health -= damage;

            if (_health <= 0)
                Die();
        }

        protected virtual void Die()
        {
            Died?.Invoke(this);
        }
    }
}
