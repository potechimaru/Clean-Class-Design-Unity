// HurtState ‚Ì—á
using State.EnemyState;
using UnityEngine;

public class EnemyHurtState : IEnemyState
{
    private readonly IEnemy _enemy;
    private readonly Animator _anim;
    private readonly IStateController _runner;

    public EnemyHurtState(IEnemy enemy, Animator anim, IStateController runner)
    {
        _enemy = enemy;
        _anim = anim;
        _runner = runner;
    }

    public void Enter()
    {
        _anim.CrossFade("Hurt", 0.05f);
    }

    public async void Tick(float dt)
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            _runner.ChangeState(StateKey.Walk);
        }
    }

    public void Exit() { }
}
