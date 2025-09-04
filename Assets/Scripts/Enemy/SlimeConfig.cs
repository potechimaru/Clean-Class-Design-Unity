using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/SlimeConfig")]
public class SlimeConfig : ScriptableObject, IEnemyConfig
{
    [Header("Status")]
    [SerializeField] private float maxHp = 10f;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float aggroDistance = 8f;

    public float MaxHp => maxHp;
    public float MoveSpeed => moveSpeed;
    public float AggroDistance => aggroDistance;
}
