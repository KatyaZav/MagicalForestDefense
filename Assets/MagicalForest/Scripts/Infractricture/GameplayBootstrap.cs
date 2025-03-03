using Assets.Gameplay.Features.Wave.Configs;
using DI.Game.Develop.CommonServices.CoroutinePerfomer;
using DI.Game.Develop.CommonServices.SceneManagment;
using DI.Game.Develop.Configs.Gameplay;
using DI.Game.Develop.DI;
using DI.Game.Develop.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBootstrap : MonoBehaviour
{
    private DIContainer _container;

    private LevelHolder _levelHolder;

    public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
    {
        _container = container;

        _levelHolder = FindObjectOfType<LevelHolder>();

        ProcessRegistrations();

        //Debug.Log($"Подгружаем ресурсы для уровня {gameplayInputArgs.LevelNumber}");
        Debug.Log("Создаем персонажа");
        Debug.Log("Сцена готова можно начинать игру");

        _container.Resolve<WaveSystem>().StartWaves();

        yield return null;
    }

    private void ProcessRegistrations()
    {
        _container.RegisterAsSingle(c => new EnemiesHolder());
        _container.RegisterAsSingle(c => new Spawner(
            _container.Resolve<EnemiesHolder>(),
            _levelHolder.GetQueue(),
            _container.Resolve<CoroutinePerformer>()));

        _container.RegisterAsSingle(c => new WaveSystem(
            _container.Resolve<Spawner>(),
            Resources.Load<GameWavesConfig>("Waves/WavesData/Level1"),
            _container.Resolve<CoroutinePerformer>()));

        _container.Initialize();
    }
}
