using UnityEngine;

public interface IPoolableEnemy
{
    void SetPool<T>(EnemyPool<T> pool) where T : MonoBehaviour, IEnemy;
}
