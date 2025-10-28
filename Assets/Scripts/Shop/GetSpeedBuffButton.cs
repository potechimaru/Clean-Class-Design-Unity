using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using TMPro;

public class GetSpeedBuffButton: MonoBehaviour, IPointerClickHandler
{
    [Inject] private readonly GetItemManager _getItemManager;
    [Inject] private readonly UpgradeCostConfig _upgradeCostConfig;
    [Inject] private readonly PlayerFacade _playerMVCFade;
    [SerializeField] private TextMeshProUGUI _cost;

    public void OnPointerClick(PointerEventData eventData)
    {
        _getItemManager.GetSpeedBuff();
        CostViewChange();
    }

    public void CostViewChange()
    {
        if (_getItemManager != null && _cost != null)
        {
            int currentLevel = _getItemManager.SpeedBuffCount;
            int nextCost = _upgradeCostConfig.GetSpeedBuffCost(currentLevel);
            _cost.text = nextCost > 0 ? nextCost.ToString() : "Sold Out";
            if (_playerMVCFade.MoneyPossession >= nextCost)
            {
                _cost.color = Color.green;
            }
            else
            {
                _cost.color = Color.red;
            }
        }
    }

}
