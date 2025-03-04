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

    private LevelDataHolder _levelHolder;

    private List<IUpdatable> _updatables = new List<IUpdatable>();

    public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
    {
        _container = container;

        _levelHolder = FindObjectOfType<LevelDataHolder>();

        if (_levelHolder == null)
            throw new Exception("Not found level");

        ProcessRegistrations();
        ProcessCreation();

        //Debug.Log($"Подгружаем ресурсы для уровня {gameplayInputArgs.LevelNumber}");
        Debug.Log("Сцена готова можно начинать игру");

        _container.Resolve<WaveSystem>().StartWaves();

        yield return null;
    }

    private void Update()
    {
        foreach (var updateble in _updatables)
            updateble.CustomUpdate(Time.deltaTime);
    }

    private void ProcessCreation()
    {
        _updatables.Add(_container.Resolve<EnemiesHolder>());
    }

    private void ProcessRegistrations()
    {
        _container.RegisterAsSingle(c => new EnemiesListHolderSystem());
        _container.RegisterAsSingle(c => new Spawner(
            _container.Resolve<EnemiesListHolderSystem>(),
            _levelHolder.GetQueue(),
            _container.Resolve<ICoroutinePerformer>()));
               
        _container.RegisterAsSingle(c => new WaveSystem(
            _container.Resolve<Spawner>(),
            Resources.Load<GameWavesConfig>("Waves/WavesData/Level1"),
            _container.Resolve<ICoroutinePerformer>()));

        _container.RegisterAsSingle(c => new EnemiesHolder(
            _container.Resolve<EnemiesListHolderSystem>()
            ));

        _container.Initialize();
    }
}
