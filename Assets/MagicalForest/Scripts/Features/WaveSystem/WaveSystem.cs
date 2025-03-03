using Assets.Gameplay.Features.Wave.Configs;
using DI.Game.Develop.CommonServices.CoroutinePerfomer;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSystem : IDisposable
{
    private Spawner _spawner;
    private GameWavesConfig _waveConfig;
    private CoroutinePerformer _performer;

    Coroutine _coroutine;
    int _currentWave = 0;

    public WaveSystem(Spawner spawner, GameWavesConfig waveConfig, CoroutinePerformer performer)
    {
        _spawner = spawner;
        _waveConfig = waveConfig;
        _performer = performer;
    }

    public void Dispose()
    {
        if (_coroutine != null)
            _performer.StopPerform(_coroutine);
    }

    public void StartWaves()
    {
        _coroutine = _performer.StartPerform(GameCycle());
    }

    private IEnumerator GameCycle()
    {
        while (true)
        {
            var currentWaveData = _waveConfig.GameWaves[_currentWave];

            for (var i = 0; i < currentWaveData.WaveEnemies.Count; i++)
            {
                _spawner.SpawnEnemies(
                    currentWaveData.WaveEnemies[i].Enemy, currentWaveData.WaveEnemies[i].Count);

                yield return new WaitUntil(() => _spawner.IsSpawning.Value == false);
            }

            //тут должен быть таймер

            yield return null;
        }
}
}
