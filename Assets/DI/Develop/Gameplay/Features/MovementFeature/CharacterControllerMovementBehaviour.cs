using DI.Game.Develop.Gameplay.Entities;
using DI.Game.Develop.Gameplay.Entities.Behaviours;
using DI.Game.Develop.Utils.Reactive;
using UnityEngine;

namespace DI.Game.Develop.Gameplay.Features.MovementFeature
{
    public class CharacterControllerMovementBehaviour : IEntityInitialize, IEntityUpdate
    {
        private CharacterController _characterController;

        private IReadOnlyVariable<float> _speed;
        private IReadOnlyVariable<Vector3> _direction;

        public void OnInit(Entity entity)
        {
            _speed = entity.GetValue<ReactiveVariable<float>>(EntityValues.MoveSpeed);
            _direction = entity.GetValue<ReactiveVariable<Vector3>>(EntityValues.MoveDirection);
            _characterController = entity.GetValue<CharacterController>(EntityValues.CharacterController);
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 velocity = _direction.Value.normalized * _speed.Value;

            _characterController.Move(velocity * deltaTime);
        }
    }
}
