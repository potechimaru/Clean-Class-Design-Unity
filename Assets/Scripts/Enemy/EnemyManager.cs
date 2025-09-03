using UnityEngine;
using System.Collections.Generic;
using VContainer.Unity;

public class EnemyManager : ITickable
{
    private readonly List<IEnemyTick> _enemies = new();
    public bool GameEnd { get; set; }

    public void Register(IEnemyTick enemy)
    {
        if (!_enemies.Contains(enemy))
            _enemies.Add(enemy);
    }

    public void Unregister(IEnemyTick enemy)
    {
        _enemies.Remove(enemy);
    }

    public void Tick()
    {
        if (GameEnd) return;

        float dt = Time.deltaTime;
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Tick(dt);
        }
    }
}
