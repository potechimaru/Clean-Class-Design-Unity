using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class TurtlePool : MonoBehaviour
{
    [SerializeField] private Turtle _prefab;
    [SerializeField] private int _defaultCapacity = 5;
    [SerializeField] private int _maxSize = 50;

    [Inject] private IObjectResolver _resolver;

    private IObjectPool<Turtle> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Turtle>(
            createFunc: () =>
            {
                var turtle = Instantiate(_prefab, transform);
                turtle.gameObject.SetActive(false);
                turtle.SetPool(_pool);
                _resolver.Inject(turtle);
                return turtle;
            },
            actionOnGet: (turtle) =>
            {
                turtle.gameObject.SetActive(true);
            },
            actionOnRelease: (turtle) =>
            {
                turtle.gameObject.SetActive(false);
                turtle.transform.SetParent(transform);
            },
            actionOnDestroy: (turtle) =>
            {
                Destroy(turtle.gameObject);
            },
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    /// <summary>
    /// Turtle‚ğ¶¬i‰Šú‰»‚ÍEnemyManager‘¤‚Ås‚¤j
    /// </summary>
    public Turtle Get(Vector3 pos, Quaternion rot)
    {
        var turtle = _pool.Get();
        turtle.transform.SetPositionAndRotation(pos, rot);
        return turtle;
    }

    public void Release(Turtle turtle) => _pool.Release(turtle);
}
