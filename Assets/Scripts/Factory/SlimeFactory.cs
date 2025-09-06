using UnityEngine;
using VContainer;

public class SlimeFactory : MonoBehaviour, IEnemyFactory
{
    [Inject] private SlimePool _pool;
    [Inject] private SlimeConfig _config;
    [SerializeField] private Transform _defaultTarget;
    [Inject] private EnemyManager _enemyManager;

    public IEnemy Create(Vector3 position, Quaternion rotation)
    {
        var slime = _pool.Get(position, rotation, _config, _defaultTarget);
        _enemyManager.Register(slime);
        return slime;
    }
}