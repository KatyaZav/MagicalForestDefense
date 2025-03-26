using DI.Game.Develop.Utils.Reactive;
using System;

public class PlayerGameplaySaves
{
    private ReactiveVariable<int> _wave;
    private ReactiveVariable<int> _coins;
    private ReactiveVariable<int> _playerHealth;

    public IReadOnlyVariable<int> Wave => _wave;
    public IReadOnlyVariable<int> Coins => _coins;
    public IReadOnlyVariable<int> Health => _playerHealth;
    
    public bool CheakMoneyEnought(int money) => _coins.Value - money > 0;

    public void AddWave()
    {
        _wave.Value += 1;
    }

    public void AddCoins(int coins)
    {
        _coins.Value += coins;
    }

    public void Remove(int coins)
    {
        if (CheakMoneyEnought(coins))
            throw new Exception("Not enought money");

        _coins.Value -= coins;
    }

    public void AddHealth(int health)
    {
        _playerHealth.Value += health; 
    }

    public void RemoveHealth(int health)
    {
        _playerHealth.Value -= health;
    }
}
