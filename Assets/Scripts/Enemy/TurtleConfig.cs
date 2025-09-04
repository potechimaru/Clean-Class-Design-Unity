using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/TurtleConfig")]
public class TurtleConfig : ScriptableObject, IEnemyConfig
{
    [Header("Status")]
    [SerializeField] private float maxHp = 20f;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float aggroDistance = 12f;

    public float MaxHp => maxHp;
    public float MoveSpeed => moveSpeed;
    public float AggroDistance => aggroDistance;
}
