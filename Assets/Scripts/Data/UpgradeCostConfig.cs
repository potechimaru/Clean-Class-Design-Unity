using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeCostConfig", menuName = "Configs/Upgrade Cost Config")]
public class UpgradeCostConfig : ScriptableObject
{
    [SerializeField] private int[] attackCosts;

    [SerializeField] private int[] shieldCosts;

    [SerializeField] private int[] beamCosts;

    [SerializeField] private int[] HPCosts;

    [SerializeField] private int[] lowHealCosts;

    [SerializeField] private int[] highHealCosts;

    [SerializeField] private int[] fullHealCosts;

    [SerializeField] private int[] RemedyCosts;

    [SerializeField] private int[] PowerBuff;

    [SerializeField] private int[] SpeedBuff;

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

    public int GetBeamCost(int level)
    {
        if (level - 1 >= 0 && level - 1 < beamCosts.Length)
            return beamCosts[level - 1];
        return -1;
    }

    public int GetHPCost(int level)
    {
        if (level - 1 >= 0 && level - 1 < HPCosts.Length)
            return HPCosts[level - 1];
        return -1;
    }

}
