using UnityEngine;

public class SlimeFactory : MonoBehaviour, IEnemyFactory
{
    [SerializeField] private EnemyPool<Slime> _pool;
    [SerializeField] private SlimeConfig _config;
    [SerializeField] private Transform _defaultTarget;
    [SerializeField] private EnemyManager _enemyManager;

    public IEnemy Create(Vector3 position, Quaternion rotation)
    {
        var slime = _pool.Get(position, rotation, _config, _defaultTarget);
        _enemyManager.Register(slime);
        return slime;
    }
}