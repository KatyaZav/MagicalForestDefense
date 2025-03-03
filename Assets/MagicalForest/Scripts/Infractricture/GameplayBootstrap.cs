using DI.Game.Develop.CommonServices.SceneManagment;
using DI.Game.Develop.DI;
using DI.Game.Develop.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBootstrap : MonoBehaviour
{
    private DIContainer _container;

    public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
    {
        _container = container;

        ProcessRegistrations();

        //Debug.Log($"���������� ������� ��� ������ {gameplayInputArgs.LevelNumber}");
        Debug.Log("������� ���������");
        Debug.Log("����� ������ ����� �������� ����");

        yield return null;
    }

    private void ProcessRegistrations()
    {
        _container.RegisterAsSingle(c => new EnemiesHolder());

        _container.Initialize();
    }
}
