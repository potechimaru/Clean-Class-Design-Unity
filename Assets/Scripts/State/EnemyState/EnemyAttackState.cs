using UnityEngine;

namespace State.EnemyState
{

    public class EnemyAttackState : IEnemyState
    {
        private readonly IEnemy _enemy;
        private readonly Animator _anim;
        private readonly IStateController _runner;

        public EnemyAttackState(IEnemy enemy, Animator anim, IStateController runner)
        {
            _enemy = enemy;
            _anim = anim;
            _runner = runner;
            Debug.Log(_enemy == null);
        }

        public void Enter()
        {
            _anim.CrossFade("Attack", 0.05f);
        }

        public void Tick(float dt)
        {
            if (!_enemy.CanAttackTarget())
            {
                _runner.ChangeState(StateKey.Walk);
            }
        }

        public void Exit() { }
    }
}
