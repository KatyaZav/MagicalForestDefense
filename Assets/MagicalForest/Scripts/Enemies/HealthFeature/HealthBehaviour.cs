using DI.Game.Develop.Utils.Reactive;
using System;

public class HealthBehaviour
{
    public event Action Died; 

    private ReactiveVariable<float> _health;

    public HealthBehaviour(ReactiveVariable<float> health)
    {
        _health = health;
    }
    
    public bool _isDied { get; private set; } = false;
    public IReadOnlyVariable<float> Health => _health;

    public void TakeDamage(float damage)
    {
        if (_isDied == true)
            throw new Exception("Try to damage died enemy");

        _health.Value -= damage;

        if (_health.Value < 0)
        {
            _isDied = true;
            Died?.Invoke();
        }
    }
}
