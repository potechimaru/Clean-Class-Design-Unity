using UnityEngine;

public interface IWaveConfig
{
    EnemySpawnData[] enemies { get; }
}

[CreateAssetMenu(menuName = "Waves/WaveConfig")]
public class WaveConfig : ScriptableObject, IWaveConfig
{
    [Header("敵の出現データリスト")]
    [SerializeField] private EnemySpawnData[] _enemies;

    public EnemySpawnData[] enemies => _enemies;
}
