using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class SlashEffectPool : MonoBehaviour
{
    [SerializeField] private SlashEffect _prefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 50;

    private IObjectPool<SlashEffect> _pool;
    [Inject] private IObjectResolver _resolver;

    private void Awake()
    {
        _pool = new ObjectPool<SlashEffect>(
            createFunc: () =>
            {
                var effect = Instantiate(_prefab, transform);
                effect.gameObject.SetActive(false);
                effect.SetPool(_pool);
                _resolver.Inject(effect);
                return effect;
            },
            actionOnGet: (effect) =>
            {
                effect.gameObject.SetActive(true);
            },
            actionOnRelease: (effect) =>
            {
                effect.gameObject.SetActive(false);
                effect.transform.SetParent(transform);
            },
            actionOnDestroy: (effect) =>
            {
                Destroy(effect.gameObject);
            },
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public SlashEffect Get(Vector3 pos, Quaternion rot)
    {
        var effect = _pool.Get();
        effect.Play(pos, rot);
        return effect;
    }

    public void Release(SlashEffect effect) => _pool.Release(effect);
}
