using Cysharp.Threading.Tasks;
using State.EnemyState;
using UnityEngine;

public interface IEnemy
{
    void Initialize(IEnemyConfig config, Transform target, PlayerFacade playerMVCFacade, EnemyStateRunner stateMachine);
    UniTask TakeDamage(float amount);
    bool IsDead { get; }

    bool CanAttackTarget();

    void MoveToTarget(float dt);

    void Tick(float deltaTime);
}
