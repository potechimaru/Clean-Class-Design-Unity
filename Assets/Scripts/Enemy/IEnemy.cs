using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IEnemy
{
    void Initialize(IEnemyConfig config, Transform target, PlayerMVCFacade playerMVCFacade);
    UniTask TakeDamage(float amount);
    bool IsDead { get; }

    bool CanAttackTarget();

    void MoveToTarget(float dt);
}
