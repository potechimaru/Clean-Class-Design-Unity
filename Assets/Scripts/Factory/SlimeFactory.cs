using UnityEngine;

public class SlimeFactory : MonoBehaviour, IEnemyFactory
{
    [SerializeField] private Slime _slimePrefab;
    [SerializeField] private Transform _defaultTarget;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private SlimeConfig _config;

    public IEnemy Create(Vector3 position, Quaternion rotation)
    {
        var slime = Instantiate(_slimePrefab, position, rotation);
        slime.Initialize(_config, _defaultTarget);

        // Manager ‚É“o˜^
        _enemyManager.Register(slime);

        return slime;
    }
}
