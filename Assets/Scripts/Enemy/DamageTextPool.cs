using UnityEngine;
using UnityEngine.Pool;

public class DamageTextPool : MonoBehaviour
{
    [SerializeField] private DamageText prefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 50;
    [SerializeField] private Transform _canvas;

    private IObjectPool<DamageText> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<DamageText>(
            createFunc: () =>
            {
                var obj = Instantiate(prefab, _canvas);
                obj.SetPool(_pool);
                obj.gameObject.SetActive(false);
                return obj;
            },
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: false,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    //ダメージUIを表示する
    public void Spawn(int damage, Vector3 worldPos)
    {
        Debug.Log($"Spawn DamageText: {damage} at {worldPos}");
        var obj = _pool.Get();
        obj.Show(damage, worldPos);
    }
}
