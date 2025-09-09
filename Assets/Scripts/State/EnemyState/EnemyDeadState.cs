using UnityEngine;

namespace State.EnemyState
{

    public class EnemyDeadState : IEnemyState
    {
        private readonly IEnemy _enemy;
        private readonly Animator _anim;
        private readonly IStateController _runner;

        public EnemyDeadState(IEnemy enemy, Animator anim, IStateController runner)
        {
            _enemy = enemy;
            _anim = anim;
            _runner = runner;
        }

        public void Enter()
        {
            _anim.CrossFade("Die", 0f);
        }

        public void Tick(float dt)
        {

        }

        public void Exit() { }
    }
}
