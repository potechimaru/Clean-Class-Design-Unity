using UnityEngine;
using DG.Tweening;

public class NextButtonBounce : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Tween _bounceTween;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        StartBounce();
    }

    void OnDisable()
    {
        _bounceTween?.Kill();
    }

    private void StartBounce()
    {
        _rectTransform.localScale = Vector3.one;

        _bounceTween = _rectTransform
            .DOScale(1.2f, 0.6f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(0.5f);
    }
}
