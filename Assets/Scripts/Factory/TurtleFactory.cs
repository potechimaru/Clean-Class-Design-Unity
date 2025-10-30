using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TurtleFactory : IEnemyFactory, IInitializable
{
    [Inject] private TurtlePool _pool;
    [Inject] private IEnemyConfigFacade _config;
    [Inject] private EnemyManager _enemyManager;
    [Inject] private PlayerFacade _playerMVCFacade;

    private Transform _defaultTarget;

    public void Initialize()
    {
        _defaultTarget = _playerMVCFacade.Transform;
    }

    public IEnemy Create(Vector3 position, Quaternion rotation)
    {
        Turtle turtle = _pool.Get(position, rotation);
        _enemyManager.Register(turtle, _config.GetTurtleConfig(), _defaultTarget, _playerMVCFacade);
        return turtle;
    }
}
