using UnityEngine;

public class PlayerModel
{
    // ----物理系 ----
    public float WalkSpeed { get; private set; } = 3.5f;
    public float RunSpeed { get; private set; } = 6.5f;
    public float Gravity { get; private set; } = -18f;

    public bool IsGrounded { get; set; } = true;
    public Vector3 Velocity { get; set; } = Vector3.zero;

    // ---- 戦闘系 ----
    public float AttackDamage { get; private set; } = 25f;

    // ---- HP 管理 ----
    public float MaxHp { get; private set; } = 100f;
    public float CurrentHp { get; private set; }

    public bool IsDead => CurrentHp <= 0f;
    public float NormalizedHp => MaxHp > 0f ? CurrentHp / MaxHp : 0f;

    public int MoneyPossession { get; set; } = 0;

    public PlayerModel()
    {
        CurrentHp = MaxHp;
    }

    /// <summary>
    /// ScriptableObject などの外部設定を適用したいときに使用（任意）
    /// </summary>
    public void SetupFromConfig(PlayerConfig cfg)
    {
        if (cfg == null) return;

        MaxHp = Mathf.Max(1f, cfg.maxHp);
        WalkSpeed = cfg.walkSpeed;
        RunSpeed = cfg.runSpeed;
        AttackDamage = cfg.attackDamage;
        Gravity = cfg.gravity;

        CurrentHp = Mathf.Min(CurrentHp, MaxHp);
    }

    /// <summary>
    /// ダメージを受ける。実際に減った量を返す。
    /// </summary>
    public float TakeDamage(float amount)
    {
        if (IsDead) return 0f;

        var before = CurrentHp;
        CurrentHp = Mathf.Max(0f, CurrentHp - Mathf.Max(0f, amount));
        //Debug.Log(CurrentHp);
        return before - CurrentHp;
    }

    /// <summary>
    /// 回復する。実際に回復した量を返す。
    /// </summary>
    public float Heal(float amount)
    {
        if (IsDead) return 0f;

        var before = CurrentHp;
        CurrentHp = Mathf.Min(MaxHp, CurrentHp + Mathf.Max(0f, amount));
        return CurrentHp - before;
    }

    /// <summary>
    /// HP を最大まで回復。
    /// </summary>
    public void ResetHp() => CurrentHp = MaxHp;
}
