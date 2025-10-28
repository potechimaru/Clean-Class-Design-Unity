using UnityEngine;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;
using State.EnemyState;

public class EnemyManager : ITickable
{
    private readonly IObjectResolver _resolver;
    private readonly List<IEnemyTick> _enemies = new();

    public bool GameEnd { get; set; }

    public EnemyManager(IObjectResolver resolver)
    {
        _resolver = resolver;
    }

    public void Register(Slime slime, IEnemyConfig config, Transform target, PlayerFacade player)
    {
        // StateMachine¶¬
        var runner = _resolver.Resolve<EnemyStateRunner>();

        // State“o˜^iSlime‚ÉˆË‘¶’“üÏ‚İj
        var anim = slime.GetComponent<Animator>();
        runner.AddState(StateKey.Idle, new EnemyIdleState(slime, anim, runner));
        runner.AddState(StateKey.Walk, new EnemyWalkState(slime, anim, runner));
        runner.AddState(StateKey.Attack, new EnemyAttackState(slime, anim, runner));
        runner.AddState(StateKey.Hurt, new EnemyHurtState(slime, anim, runner));
        runner.AddState(StateKey.Dead, new EnemyDeadState(slime, anim, runner));

        // Slime‰Šú‰»
        slime.Initialize(config, target, player, runner);

        if (!_enemies.Contains(slime))
            _enemies.Add(slime);
    }

    public void Unregister(IEnemyTick enemy)
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
