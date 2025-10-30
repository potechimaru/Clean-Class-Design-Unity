using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

public class WaveRunner : IStartable, IDisposable
{
    private readonly IWaveConfig _waveConfig;
    private readonly EnemyFactoryRegistry _enemyFactoryRegistry;

    private CancellationTokenSource _cts;

    public WaveRunner(
        IWaveConfig waveConfig,
        EnemyFactoryRegistry enemyFactoryRegistry
        )
    {
        _waveConfig = waveConfig;
        _enemyFactoryRegistry = enemyFactoryRegistry;
    }

    public void Start()
    {
        _cts = new CancellationTokenSource();
        RunWave(_cts.Token).Forget();
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private async UniTaskVoid RunWave(CancellationToken token)
    {
        float startTime = Time.time;

        foreach (var enemy in _waveConfig.enemies)
        {
            float wait = enemy.spawnTime + 1f - (Time.time - startTime);
            if (wait > 0f)
            {
                await UniTask.Delay(
                    millisecondsDelay: (int)(wait * 1000),
                    cancellationToken: token
                );
            }

            if (token.IsCancellationRequested) break;

            _enemyFactoryRegistry.Create(enemy.enemyType, enemy.spawnPosition, Quaternion.identity);

        }
    }
}
