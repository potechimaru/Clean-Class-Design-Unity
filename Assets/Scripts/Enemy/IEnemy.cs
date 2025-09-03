using UnityEngine;

public interface IEnemy
{
    void Initialize(SlimeConfig config, Transform target);
    void TakeDamage(float amount);
    bool IsDead { get; }

    bool CanAttackTarget();

    void MoveTowardsTarget(float dt);
}
