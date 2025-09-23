using UnityEngine;
using VContainer;

public class GetItemManager
{
    [Inject] private UpgradeCostConfig _upgradeCostConfig;
    [Inject] private PlayerMVCFacade _playerMVCFacade;

    public int LowHealCount { get; private set; } = 1;
    public int HighHealCount { get; private set; } = 1;
    public int FullHealCount { get; private set; } = 1;
    public int RemedyCount { get; private set; } = 1;
    public int PowerBuffCount { get; private set; } = 1;
    public int SpeedBuffCount { get; private set; } = 1;

    private int _lowHealAmount = 30;
    private int _highHealAmount = 100;
    private int _fullHealAmount = 100000;
    private int _remedyAmount = 10;
    private int _powerBuffAmount = 10;
    private int _speedBuffAmount = 10;

    public void GetLowHeal()
    {
        int cost = _upgradeCostConfig.GetLowHealCost(1);
        if (cost > 0 && _playerMVCFacade.MoneyPossession >= cost)
        {
            _playerMVCFacade.UpdateMoneyPossesion(-cost);
            _playerMVCFacade.Heal(_lowHealAmount);
        }
    }

    public void GetHighHeal()
    {
        int cost = _upgradeCostConfig.GetHighHealCost(1);
        if (cost > 0 && _playerMVCFacade.MoneyPossession >= cost)
        {
            _playerMVCFacade.UpdateMoneyPossesion(-cost);
            _playerMVCFacade.Heal(_highHealAmount);
        }
    }

    public void GetFullHeal()
    {
        int cost = _upgradeCostConfig.GetFullHealCost(1);
        if (cost > 0 && _playerMVCFacade.MoneyPossession >= cost)
        {
            _playerMVCFacade.UpdateMoneyPossesion(-cost);
            _playerMVCFacade.Heal(_fullHealAmount);
        }
    }

    public void GetRemedy()
    {
        int cost = _upgradeCostConfig.GetRemedyCost(1);
        if (cost > 0 && _playerMVCFacade.MoneyPossession >= cost)
        {
            _playerMVCFacade.UpdateMoneyPossesion(-cost);
            _playerMVCFacade.Heal(_remedyAmount);
        }
    }

    public void GetPowerBuff()
    {
        int cost = _upgradeCostConfig.GetPowerBuffCost(1);
        if (cost > 0 && _playerMVCFacade.MoneyPossession >= cost)
        {
            _playerMVCFacade.UpdateMoneyPossesion(-cost);
            _playerMVCFacade.PowerBuff(_powerBuffAmount);
        }
    }

    public void GetSpeedBuff()
    {
        int cost = _upgradeCostConfig.GetSpeedBuffCost(1);
        if (cost > 0 && _playerMVCFacade.MoneyPossession >= cost)
        {
            _playerMVCFacade.UpdateMoneyPossesion(-cost);
            _playerMVCFacade.SpeedBuff(_speedBuffAmount);
        }
    }
}