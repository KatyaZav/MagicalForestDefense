using DI.Game.Develop.DI;
using DI.Game.Develop.Gameplay.Entities;
using DI.Game.Develop.Utils.Reactive;
using UnityEngine;

namespace DI.Game.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        private Entity _ghost;

        public void StartProcess(DIContainer container)
        {
            _container = container;

            _ghost = _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero);

            _ghost.TryGetValue(EntityValues.MoveSpeed, out ReactiveVariable<float> moveSpeed);
            Debug.Log($"Скорость созданного призрака: {moveSpeed.Value}");
        }

        private void Update()
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if(_ghost != null)
            {
                _ghost.TryGetValue(EntityValues.MoveDirection, out ReactiveVariable<Vector3> moveDirection);
                _ghost.TryGetValue(EntityValues.RotationDirection, out ReactiveVariable<Vector3> rotationDirection);

                moveDirection.Value = input;
                rotationDirection.Value = input;
            }
        }
    }
}
