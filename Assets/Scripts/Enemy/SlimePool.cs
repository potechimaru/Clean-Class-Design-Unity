using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class SlimePool : MonoBehaviour
{
    [SerializeField] private Slime _prefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private IObjectPool<Slime> _pool;
    [Inject] private IObjectResolver _resolver;

    private void Start()
    {
        _pool = new ObjectPool<Slime>(
            createFunc: () =>
            {
                var slime = Instantiate(_prefab, transform);
                slime.gameObject.SetActive(false);
                slime.SetPool(_pool);
                _resolver.Inject(slime);
                return slime;
            },
            actionOnGet: (slime) =>
            {
                slime.gameObject.SetActive(true);
            },
            actionOnRelease: (slime) =>
            {
                slime.gameObject.SetActive(false);
                slime.transform.SetParent(transform);
            },
            actionOnDestroy: (slime) =>
            {
                Destroy(slime.gameObject);
            },
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public Slime Get(Vector3 pos, Quaternion rot)
    {
        var slime = _pool.Get();
        slime.transform.SetPositionAndRotation(pos, rot);
        return slime;
    }

    public void Release(Slime slime) => _pool.Release(slime);
}
