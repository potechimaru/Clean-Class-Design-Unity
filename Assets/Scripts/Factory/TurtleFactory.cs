using UnityEngine;
using VContainer;

public class TurtleFactory : MonoBehaviour, IEnemyFactory
{
    [SerializeField] private TurtlePool _pool;
    [SerializeField] private TurtleConfig _config;
    [SerializeField] private Transform _defaultTarget;
    [Inject] private EnemyManager _enemyManager;

    public IEnemy Create(Vector3 position, Quaternion rotation)
    {
        var slime = _pool.Get(position, rotation, _config, _defaultTarget);
        _enemyManager.Register(slime);
        return slime;
    }
}