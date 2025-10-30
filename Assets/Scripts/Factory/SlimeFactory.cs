using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SlimeFactory : IEnemyFactory, IInitializable
{
    [Inject] private SlimePool _pool;
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
        Slime slime = _pool.Get(position, rotation);

        _enemyManager.Register(slime, _config.GetSlimeConfig(), _defaultTarget, _playerMVCFacade);
        return slime;
    }
}
