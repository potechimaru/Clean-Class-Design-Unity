using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/SlimeConfig")]
public class SlimeConfig : ScriptableObject, IEnemyConfig
{
    [Header("Status")]
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float aggroDistance = 8f;
    [SerializeField] private float attackDamage = 10f;

    public float MaxHp => maxHp;
    public float MoveSpeed => moveSpeed;
    public float AggroDistance => aggroDistance;
    public float AttackDamage => attackDamage;
}
