public interface IEnemyConfig
{
    float MaxHp { get; }
    float MoveSpeed { get; }
    float AggroDistance { get; }
    float AttackDamage { get; }
    int DropMoney { get; }
}