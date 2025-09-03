using UnityEngine;

namespace State.EnemyState
{

    public class EnemyWalkState : IEnemyState
    {
        private readonly IEnemy _slime;
        private readonly Animator _anim;
        private readonly IStateController _runner;

        public EnemyWalkState(Slime slime, Animator anim, IStateController runner)
        {
            _slime = slime;
            _anim = anim;
            _runner = runner;
        }

        public void Enter()
        {
            _anim.CrossFade("Walk", 0.1f);
        }

        public void Tick(float dt)
        {
            _slime.MoveTowardsTarget(dt);

            // ğŒ‚Å‘JˆÚ—áFUŒ‚‰Â”\‚È‚çAttack‚Ö
            if (_slime.CanAttackTarget())
            {
                _runner.ChangeState(StateKey.Attack);
            }
        }

        public void Exit() { }
    }
}
