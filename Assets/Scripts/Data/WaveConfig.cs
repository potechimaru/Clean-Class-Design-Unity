using UnityEngine;

[CreateAssetMenu(menuName = "Waves/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [Header("敵の出現データリスト")]
    public EnemySpawnData[] enemies;
}
