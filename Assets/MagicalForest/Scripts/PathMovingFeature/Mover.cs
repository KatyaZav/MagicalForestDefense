using Unity.VisualScripting;
using UnityEngine;

public class Mover : IUpdatable
{
    private Transform _transform;
    private float _speed;

    private Vector3 _targetPosition;

    public Mover(Transform rb, float speed)
    {
        _transform = rb;
        _speed = speed;
    }

    public Vector3 CurrentPosition => _transform.transform.position;

    public void MoveTo(Vector3 targetPosition)
    {
        _transform.gameObject.transform.LookAt(targetPosition);
        _targetPosition = new Vector3(targetPosition.x, _transform.transform.position.y, targetPosition.z);
    }

    public void CustomUpdate(float deltaTime)
    {
        if (_targetPosition == null)
            throw new System.Exception("Not add target positiopn to move");

        Vector3 direction = (_targetPosition - _transform.transform.position).normalized;
        _transform.transform.position += direction * _speed * Time.deltaTime;
    }
}
