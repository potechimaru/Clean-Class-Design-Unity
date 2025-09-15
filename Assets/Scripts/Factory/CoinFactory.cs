using UnityEngine;
using VContainer;

public class CoinFactory : MonoBehaviour
{
    [Inject] private CoinPool _coinPool;

    /// <summary>
    /// コインを生成する
    /// </summary>
    public Coin Create(Vector3 position, int amount)
    {
        // コインをプールから取得
        return _coinPool.Get(position, Quaternion.identity, amount);
    }
}