using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeCostConfig", menuName = "Configs/Upgrade Cost Config")]
public class UpgradeCostConfig : ScriptableObject
{
    [SerializeField] private int[] attackCosts;

    [SerializeField] private int[] shieldCosts;

    /// <summary>
    /// 指定したレベルに必要なコストを返す
    /// </summary>
    public int GetAttackCost(int level)
    {
        if (level - 1 >= 0 && level - 1 < attackCosts.Length)
            return attackCosts[level - 1];
        return -1;
    }

    public int GetShieldCost(int level)
    {
        if (level - 1 >= 0 && level - 1 < shieldCosts.Length)
            return shieldCosts[level - 1];
        return -1;
    }
}
