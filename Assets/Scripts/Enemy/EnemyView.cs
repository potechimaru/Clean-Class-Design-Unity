using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private HPBar hpBar;  // Inspectorでアタッチ

    private IEnemy _enemy;
    private float _maxHp;

    /// <summary>
    /// 敵本体から初期化時に呼ばれる
    /// </summary>
    public void Initialize(IEnemy enemy, float maxHp)
    {
        _enemy = enemy;
        _maxHp = maxHp;
        hpBar.SetFill(1f); // 初期化時は満タン
    }

    /// <summary>
    /// ダメージを受けた時に呼ぶ
    /// </summary>
    public void UpdateHp(float currentHp)
    {
        float ratio = currentHp / _maxHp;
        hpBar.SetFill(ratio);
    }
}
