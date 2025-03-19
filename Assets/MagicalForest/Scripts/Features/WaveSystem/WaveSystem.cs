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
    private ICoroutinePerformer _performer;

    Coroutine _coroutine;
    int _currentWave = 0;

    public WaveSystem(Spawner spawner, GameWavesConfig waveConfig, ICoroutinePerformer performer)
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
        for (var waveCount = 0; waveCount < _waveConfig.GameWaves.Count; waveCount++)
        {
            Debug.Log($"Start new wave number {waveCount}");
            
            _currentWave = waveCount;
            var currentWaveData = _waveConfig.GameWaves[waveCount];

            for (var i = 0; i < currentWaveData.WaveEnemies.Count; i++)
            {
                Debug.Log($"Start spawning {currentWaveData.WaveEnemies[i].Enemy}");

                _spawner.SpawnEnemies(
                    currentWaveData.WaveEnemies[i].Enemy, currentWaveData.WaveEnemies[i].Count);

                yield return new WaitUntil(() => _spawner.IsSpawning.Value == false);
                yield return new WaitForSeconds(1.5f);
            }

            yield return new WaitForSeconds(currentWaveData.WaitTime);
        }

        Debug.Log("SpawnEnded");
    }
}
