using UnityEngine;

namespace State.EnemyState
{

    public class EnemyDeadState : IEnemyState
    {
        private readonly IEnemy _slime;
        private readonly Animator _anim;
        private readonly IStateController _runner;

        public EnemyDeadState(Slime slime, Animator anim, IStateController runner)
        {
            _slime = slime;
            _anim = anim;
            _runner = runner;
        }

        public void Enter()
        {
            _anim.CrossFade("Dead", 0.05f);
        }

        public void Tick(float dt)
        {

        }

        public void Exit() { }
    }
}
