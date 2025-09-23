using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using TMPro;

public class GetHighHealButton: MonoBehaviour, IPointerClickHandler
{
    [Inject] private readonly GetItemManager _getItemManager;
    [Inject] private readonly UpgradeCostConfig _upgradeCostConfig;
    [Inject] private readonly PlayerMVCFacade _playerMVCFacade;
    [SerializeField] private TextMeshProUGUI _cost;

    public void OnPointerClick(PointerEventData eventData)
    {
        _getItemManager.GetHighHeal();
        CostViewChange();
    }

    public void CostViewChange()
    {
        if (_getItemManager != null && _cost != null)
        {
            int currentLevel = _getItemManager.HighHealCount;
            int nextCost = _upgradeCostConfig.GetHighHealCost(currentLevel);
            _cost.text = nextCost > 0 ? nextCost.ToString() : "Sold Out";
            if (_playerMVCFacade.MoneyPossession >= nextCost)
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
