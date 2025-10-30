using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactoryRegistry
{
    IEnemy Create(Vector3 position, Quaternion rotation);
}

public class EnemyFactoryRegistry
{
    private readonly Dictionary<EnemyType, IEnemyFactory> _factories;

    public EnemyFactoryRegistry(Dictionary<EnemyType, IEnemyFactory> factories)
    {
        _factories = factories;
    }

    /// <summary>
    /// 指定タイプの敵を生成
    /// </summary>
    public IEnemy Create(EnemyType type, Vector3 position, Quaternion rotation)
    {
        if (_factories.TryGetValue(type, out var factory))
        {
            return factory.Create(position, rotation);
        }

        return null;
    }
}
