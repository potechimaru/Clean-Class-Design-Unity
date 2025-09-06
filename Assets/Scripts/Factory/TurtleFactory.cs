using UnityEngine;
using VContainer;

public class TurtleFactory : MonoBehaviour, IEnemyFactory
{
    [Inject] private TurtlePool _pool;
    [Inject] private TurtleConfig _config;
    [SerializeField] private Transform _defaultTarget;
    [Inject] private EnemyManager _enemyManager;

    public IEnemy Create(Vector3 position, Quaternion rotation)
    {
        var turtle = _pool.Get(position, rotation, _config, _defaultTarget);
        _enemyManager.Register(turtle);
        return turtle;
    }
}