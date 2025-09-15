using UnityEngine;
using DG.Tweening;

public class ItemFloat : MonoBehaviour
{
    private float floatStrength = 0.3f;       // 上下に移動する距離
    private float floatDuration = 1.5f;       // １往復にかかる時間
    private float rotationSpeed = 45f;        // １秒あたりの回転速度

    private Tween moveTween;
    private Tween rotateTween;

    private void Start()
    {
        // 上下にループ移動（Y方向）
        moveTween = transform.DOMoveY(transform.position.y + floatStrength, floatDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        // Y軸回転をループ（ぐるぐる）
        rotateTween = transform.DORotate(new Vector3(0, 360, 0), 360f / rotationSpeed, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    /// <summary>
    /// シーン遷移時にキル
    /// </summary>
    private void OnDestroy()
    {
        moveTween?.Kill();
        rotateTween?.Kill();
    }
}