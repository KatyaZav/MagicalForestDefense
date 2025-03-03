using Unity.VisualScripting;
using UnityEngine;

public class Mover : IUpdateble
{
    private Rigidbody _rigidbody;
    private float _speed;

    private Vector3 _targetPosition;

    public Mover(Rigidbody rb, float speed)
    {
        _rigidbody = rb;
        _speed = speed;
    }

    public Vector3 CurrentPosition => _rigidbody.transform.position;

    public void MoveTo(Vector3 targetPosition)
    {
        _rigidbody.gameObject.transform.LookAt(targetPosition);
        _targetPosition = new Vector3(targetPosition.x, _rigidbody.transform.position.y, targetPosition.z);
    }

    public void Update(float deltaTime)
    {
        if (_targetPosition == null)
            throw new System.Exception("Not add target positiopn to move");

        Vector3 direction = (_targetPosition - _rigidbody.transform.position).normalized;
        _rigidbody.transform.position += direction * _speed * Time.deltaTime;
    }
}
