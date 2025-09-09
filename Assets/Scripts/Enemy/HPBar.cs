using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// HPBarの減少アニメーションを定義する
/// </summary>
public class HPBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private Tween currentTween;

    /// <summary>
    /// 緑の画像の横幅を残りHPの割合に応じて減らしていく。
    /// </summary>
    /// <param name="fillAmount">残りHPの割合 (0〜1)</param>
    public void SetFill(float fillAmount)
    {
        fillAmount = Mathf.Clamp01(fillAmount);

        // 既存のアニメーションを止める
        currentTween?.Kill();

        // 色変更
        if (fillAmount <= 0.2f)
        {
            fillImage.color = Color.red;
        }
        else if (fillAmount <= 0.5f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.green;
        }

        // アニメーション開始（0.3秒かけてfillAmountに変化）
        currentTween = fillImage.DOFillAmount(fillAmount, 0.3f).SetEase(Ease.OutQuad);
    }

    /// <summary>
    /// シーン遷移対策でアニメーションをキル
    /// </summary>
    private void OnDestroy()
    {
        currentTween?.Kill();
    }
}
