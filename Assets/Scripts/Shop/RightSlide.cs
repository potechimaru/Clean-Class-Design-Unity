using UnityEngine;
using UnityEngine.EventSystems;

public class RightSlide : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MenuSlideManager manager;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("RightSide");
        manager.ShowNext();
    }
}
