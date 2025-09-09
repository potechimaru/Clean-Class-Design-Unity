using UnityEngine;

namespace State.EnemyState
{

    public class EnemyWalkState : IEnemyState
    {
        private readonly IEnemy _enemy;
        private readonly Animator _anim;
        private readonly IStateController _runner;

        public EnemyWalkState(IEnemy enemy, Animator anim, IStateController runner)
        {
            _enemy = enemy;
            _anim = anim;
            _runner = runner;
        }

        public void Enter()
        {
            _anim.CrossFade("WalkFWD", 0.1f);
        }

        public void Tick(float dt)
        {
            //Debug.Log("EnemyWalkState Tick");
            _enemy.MoveToTarget(dt);

            // UŒ‚‰Â”\‚È‚çAttack‚Ö
            if (_enemy.CanAttackTarget())
            {
                _runner.ChangeState(StateKey.Attack);
            }
        }

        public void Exit() { }
    }
}
