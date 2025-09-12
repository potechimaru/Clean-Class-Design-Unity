using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Pool;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageUI;
    private IObjectPool<DamageText> _pool;

    public void SetPool(IObjectPool<DamageText> pool)
    {
        _pool = pool;
    }

    // 初期化して表示開始
    public void Show(int damage, Vector3 worldPos)
    {
        damageUI.text = damage.ToString();

        transform.position = worldPos;

        gameObject.SetActive(true);

        // アニメーション例：上に移動して消える
        damageUI.color = Color.white;
        transform.DOMoveY(transform.position.y + 0.3f, 0.4f).SetRelative();
        damageUI.DOFade(0f, 1.2f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() => _pool.Release(this));
    }
}
