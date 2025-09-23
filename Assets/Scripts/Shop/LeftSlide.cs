using UnityEngine;
using UnityEngine.EventSystems;

public class LeftSlide : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MenuSlideManager manager;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("LeftSlide");
        manager.ShowPrevious();
    }
}
