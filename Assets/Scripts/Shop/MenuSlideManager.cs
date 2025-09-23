using UnityEngine;
using DG.Tweening;

public class MenuSlideManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels;
    [SerializeField] private float slideDistance = 2000f;
    [SerializeField] private float duration = 0.4f;

    private int currentIndex = 0;

    public void ShowNext()
    {
        if (currentIndex < panels.Length - 1)
        {
            SlideTo(currentIndex + 1, true);
        }
    }

    public void ShowPrevious()
    {
        if (currentIndex > 0)
        {
            SlideTo(currentIndex - 1, false);
        }
    }

    private void SlideTo(int newIndex, bool toLeft)
    {
        var currentPanel = panels[currentIndex];
        var nextPanel = panels[newIndex];

        // 出ていくパネル
        float outTargetX = toLeft ? -slideDistance : slideDistance;
        currentPanel.DOAnchorPosX(outTargetX, duration).SetEase(Ease.InOutQuad)
            .OnComplete(() => currentPanel.gameObject.SetActive(false));

        // 入ってくるパネル
        float inStartX = toLeft ? slideDistance : -slideDistance;
        nextPanel.gameObject.SetActive(true);
        nextPanel.anchoredPosition = new Vector2(inStartX, nextPanel.anchoredPosition.y);
        nextPanel.DOAnchorPosX(0, duration).SetEase(Ease.InOutQuad);

        currentIndex = newIndex;
    }
}
