using UnityEngine;
using UnityEngine.Pool;

public class SlimePool : MonoBehaviour
{
    [SerializeField] private Slime _prefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private IObjectPool<Slime> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Slime>(
            createFunc: () =>
            {
                var slime = Instantiate(_prefab, transform);
                slime.gameObject.SetActive(false);
                slime.SetPool(_pool);
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

    public Slime Get(Vector3 pos, Quaternion rot, SlimeConfig config, Transform target)
    {
        var slime = _pool.Get();
        slime.transform.SetPositionAndRotation(pos, rot);
        slime.Initialize(config, target);
        return slime;
    }

    public void Release(Slime slime) => _pool.Release(slime);
}
