using UnityEngine;

public interface IEnemyFactory
{
    IEnemy Create(Vector3 position, Quaternion rotation);
}
