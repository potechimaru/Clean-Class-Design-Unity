using UnityEngine;
using VContainer;

public class UpgradeManager
{
    [Inject] private UpgradeCostConfig _upgradeCostConfig;
    [Inject] private PlayerMVCFacade _facade;

    public int AttackLevel { get; private set; } = 1;
    public int ShieldLevel { get; private set; } = 1;

    public void TryUpgradeAttack()
    {
        int cost = _upgradeCostConfig.GetAttackCost(AttackLevel);
        if (cost > 0 && _facade.MoneyPossession >= cost)
        {
            _facade.UpdateMoneyPossesion(-cost);
            AttackLevel++;
            Debug.Log($"Attack upgraded! New level: {AttackLevel}");
        }
        else
        {
            Debug.Log("Not enough money for Attack upgrade!");
        }
    }

    public void TryUpgradeShield()
    {
        int cost = _upgradeCostConfig.GetShieldCost(ShieldLevel);
        if (cost > 0 && _facade.MoneyPossession >= cost)
        {
            _facade.UpdateMoneyPossesion(-cost);
            ShieldLevel++;
            Debug.Log($"Shield upgraded! New level: {ShieldLevel}");
        }
        else
        {
            Debug.Log("Not enough money for Shield upgrade!");
        }
    }
}
