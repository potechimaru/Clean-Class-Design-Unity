using TMPro;
using UnityEngine;
using VContainer;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private HPBar _hpBar;
    [Inject] private DamageTextPool _damageTextPool;

    private IEnemy _enemy;
    private float _maxHp;

    /// <summary>
    /// 敵本体から初期化時に呼ばれる
    /// </summary>
    public void Initialize(IEnemy enemy, float maxHp)
    {
        _enemy = enemy;
        _maxHp = maxHp;
        _hpBar.SetFill(1f);

    }

    /// <summary>
    /// ダメージを受けた時に呼ぶ
    /// </summary>
    public void UpdateHp(float currentHp)
    {
        float ratio = currentHp / _maxHp;
        _hpBar.SetFill(ratio);
    }

    public void ShowDamage(int damage, Vector3 worldPos)
    {
        //Debug.Log(_damageTextPool == null);
        _damageTextPool.Spawn(damage, worldPos);
    }
}
