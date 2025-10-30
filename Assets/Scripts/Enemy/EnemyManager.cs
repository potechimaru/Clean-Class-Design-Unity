using UnityEngine;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;
using State.EnemyState;

public class EnemyManager : ITickable
{
    private readonly IObjectResolver _resolver;
    private readonly List<IEnemy> _enemies = new();

    public bool GameEnd { get; set; }

    public EnemyManager(IObjectResolver resolver)
    {
        _resolver = resolver;
    }

    public void Register(IEnemy enemy, IEnemyConfig config, Transform target, PlayerFacade player)
    {
        var runner = CreateStateRunner(enemy);
        enemy.Initialize(config, target, player, runner);
        AddEnemy(enemy);
    }

    private void AddEnemy(IEnemy enemy)
    {
        if (!_enemies.Contains(enemy))
            _enemies.Add(enemy);
    }

    private EnemyStateRunner CreateStateRunner(IEnemy enemy)
    {
        var runner = _resolver.Resolve<EnemyStateRunner>();
        var anim = (enemy as MonoBehaviour).GetComponent<Animator>();

        runner.AddState(StateKey.Idle, new EnemyIdleState(enemy, anim, runner));
        runner.AddState(StateKey.Walk, new EnemyWalkState(enemy, anim, runner));
        runner.AddState(StateKey.Attack, new EnemyAttackState(enemy, anim, runner));
        runner.AddState(StateKey.Hurt, new EnemyHurtState(enemy, anim, runner));
        runner.AddState(StateKey.Dead, new EnemyDeadState(enemy, anim, runner));

        return runner;
    }

    public void Unregister(IEnemy enemy)
    {
        _enemies.Remove(enemy);
    }

    public void Tick()
    {
        if (GameEnd) return;

        float dt = Time.deltaTime;
        foreach (var e in _enemies)
            e.Tick(dt);
    }
}
