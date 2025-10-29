using UnityEngine;
using VContainer;

public class SlimeFactory : MonoBehaviour, IEnemyFactory
{
    [Inject] private SlimePool _pool;
    [Inject] private SlimeConfig _config;
    [Inject] private EnemyManager _enemyManager;
    [Inject] private PlayerFacade _playerMVCFacade;

    [SerializeField] private Transform _defaultTarget;

    public IEnemy Create(Vector3 position, Quaternion rotation)
    {
        var slime = _pool.Get(position, rotation);

        _enemyManager.Register(slime, _config, _defaultTarget, _playerMVCFacade);
        return slime;
    }
}
