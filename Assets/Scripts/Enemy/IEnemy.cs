using UnityEngine;

public interface IEnemy
{
    void Initialize(IEnemyConfig config, Transform target);
    void TakeDamage(float amount);
    bool IsDead { get; }

    bool CanAttackTarget();

    void MoveToTarget(float dt);
}
