using UnityEngine;

namespace State.EnemyState
{

    public class EnemyAttackState : IEnemyState
    {
        private readonly IEnemy _slime;
        private readonly Animator _anim;
        private readonly IStateController _runner;

        public EnemyAttackState(Slime slime, Animator anim, IStateController runner)
        {
            _slime = slime;
            _anim = anim;
            _runner = runner;
        }

        public void Enter()
        {
            _anim.CrossFade("Attack", 0.05f);
        }

        public void Tick(float dt)
        {
            if (!_slime.CanAttackTarget())
            {
                _runner.ChangeState(StateKey.Walk);
            }
        }

        public void Exit() { }
    }
}
