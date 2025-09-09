using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float maxHp = 100f;
    public float walkSpeed = 3.5f;
    public float runSpeed = 6.5f;
    public float attackDamage = 20f;

    public float gravity = -18f;
}
