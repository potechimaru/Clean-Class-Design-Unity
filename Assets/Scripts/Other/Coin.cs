using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class Coin : MonoBehaviour
{
    private IObjectPool<Coin> _pool;
    private int _amount;
    [Inject] private PlayerMVCFacade _playerMVCFacade;

    public void SetPool(IObjectPool<Coin> pool) => _pool = pool;

    public void Initialize(int amount)
    {
        _amount = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player got {_amount} coins!");
            // Š‹à‰ÁZ‚Ìˆ—‚ğ“ü‚ê‚é

            _playerMVCFacade.UpdateMoneyPossesion(_amount);
            _pool?.Release(this); // ƒv[ƒ‹‚É•Ô‹p
        }
    }
}
