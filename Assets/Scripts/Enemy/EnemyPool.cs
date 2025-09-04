using UnityEngine;
using UnityEngine.Pool;

// すべての敵クラスで使えるジェネリックプール
public class EnemyPool<T> : MonoBehaviour where T : MonoBehaviour, IEnemy
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () =>
            {
                var enemy = Instantiate(_prefab, transform);
                enemy.gameObject.SetActive(false);

                // IEnemy 実装にプール参照を渡す
                if (enemy is IPoolableEnemy poolable)
                    poolable.SetPool(this);

                return enemy;
            },
            actionOnGet: (enemy) =>
            {
                enemy.gameObject.SetActive(true);
            },
            actionOnRelease: (enemy) =>
            {
                enemy.gameObject.SetActive(false);
                enemy.transform.SetParent(transform);
            },
            actionOnDestroy: (enemy) =>
            {
                Destroy(enemy.gameObject);
            },
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public T Get(Vector3 pos, Quaternion rot, SlimeConfig config, Transform target)
    {
        var enemy = _pool.Get();
        enemy.transform.SetPositionAndRotation(pos, rot);
        enemy.Initialize(config, target);
        return enemy;
    }

    public void Release(T enemy) => _pool.Release(enemy);
}
