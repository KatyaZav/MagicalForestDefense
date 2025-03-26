using UnityEngine;

namespace Assets.Gameplay.Features.Towers
{
    public interface ITowerDamage
    {
        public void Attack(Collider[] enemies);
    }
}