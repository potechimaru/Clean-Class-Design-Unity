using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class ShieldUpgradeButton : MonoBehaviour, IPointerClickHandler
{
    [Inject] private UpgradeManager upgradeManager;
    [Inject] private PlayerFacade _playerMVCFacade;
    [Inject] private UpgradeCostConfig _upgradeCostConfig;
    [SerializeField] private TextMeshProUGUI _lv;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _lvAmount;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (upgradeManager != null)
        {
            upgradeManager.TryUpgradeShield();
            LevelViewChange();
        }
    }

    public void LevelViewChange()
    {
        if (upgradeManager != null && _lv != null && _cost != null)
        {
            int currentLevel = upgradeManager.ShieldLevel;
            _lvAmount.text = currentLevel.ToString();
            int nextCost = _upgradeCostConfig.GetShieldCost(currentLevel);
            _cost.text = nextCost > 0 ? nextCost.ToString() : "MAX";
            if (_playerMVCFacade.MoneyPossession >= nextCost)
            {
                _lv.color = Color.green;
                _cost.color = Color.green;
            }
            else
            {
                _lv.color = Color.red;
                _cost.color = Color.red;
            }
        }
    }
}
