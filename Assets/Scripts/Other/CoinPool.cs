using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class CoinPool : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _defaultCapacity = 20;
    [SerializeField] private int _maxSize = 200;

    private IObjectPool<Coin> _pool;
    [Inject] private IObjectResolver _resolver;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () =>
            {
                var coin = Instantiate(_prefab, transform);
                coin.gameObject.SetActive(false);
                coin.SetPool(_pool);
                _resolver.Inject(coin);
                return coin;
            },
            actionOnGet: (coin) =>
            {
                coin.gameObject.SetActive(true);
            },
            actionOnRelease: (coin) =>
            {
                coin.gameObject.SetActive(false);
                coin.transform.SetParent(transform);
            },
            actionOnDestroy: (coin) =>
            {
                Destroy(coin.gameObject);
            },
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public Coin Get(Vector3 pos, Quaternion rot, int amount)
    {
        var coin = _pool.Get();
        coin.transform.SetPositionAndRotation(pos + Vector3.up * 0.6f, rot);
        coin.Initialize(amount);
        return coin;
    }

    public void Release(Coin coin) => _pool.Release(coin);
}
