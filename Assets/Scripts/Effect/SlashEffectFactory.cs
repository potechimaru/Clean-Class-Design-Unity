using UnityEngine;
using VContainer;

public class SlashEffectFactory : MonoBehaviour
{
    [Inject] private SlashEffectPool _pool;

    public void Create(Vector3 position, Quaternion rotation)
    {
        _pool.Get(position, rotation);
    }
}
