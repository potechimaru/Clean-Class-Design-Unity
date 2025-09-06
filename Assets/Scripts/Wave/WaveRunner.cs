using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

public class WaveRunner : IStartable, IDisposable
{
    private readonly WaveConfig _waveConfig;
    private readonly SlimeFactory _slimeFactory;
    private readonly TurtleFactory _turtleFactory;

    private CancellationTokenSource _cts;

    public WaveRunner(
        WaveConfig waveConfig,
        SlimeFactory slimeFactory,
        TurtleFactory turtleFactory)
    {
        _waveConfig = waveConfig;
        _slimeFactory = slimeFactory;
        _turtleFactory = turtleFactory;
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

            switch (enemy.enemyType)
            {
                case EnemyType.Slime:
                    _slimeFactory.Create(enemy.spawnPosition, Quaternion.identity);
                    break;

                case EnemyType.Turtle:
                    _turtleFactory.Create(enemy.spawnPosition, Quaternion.identity);
                    break;
            }
        }
    }
}
