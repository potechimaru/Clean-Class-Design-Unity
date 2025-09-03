using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/SlimeConfig")]
public class SlimeConfig : ScriptableObject
{
    [Header("Status")]
    public float maxHp = 20f;
    public float moveSpeed = 2.0f;
    public float aggroDistance = 12f;

}