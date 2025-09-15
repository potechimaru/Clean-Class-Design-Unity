using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using UniRx;

namespace State.PlayerState
{
    public class IdleState : IPlayerState
    {
        private readonly PlayerMVCFacade _facade;
        private readonly IStateController _stateController;
        private readonly Func<Vector2> _moveGetter;
        private readonly Func<bool> _runGetter;
        private readonly Func<bool> _attackGetter;
        private readonly Func<bool> _shieldGetter;

        public IdleState(PlayerMVCFacade facade, IStateController stateController,
                         Func<Vector2> moveGetter, Func<bool> runGetter, Func<bool> attackGetter, Func<bool> shieldGetter)
        {
            _facade = facade;
            _stateController = stateController;
            _moveGetter = moveGetter;
            _runGetter = runGetter;
            _attackGetter = attackGetter;
            _shieldGetter = shieldGetter;
        }

        public async UniTask Enter()
        {
            _facade.PlayAnimation("Idle_Normal", 0.1f);
            _facade.Velocity = Vector3.zero;
        }

        public async UniTask Tick()
        {
            var mv = _moveGetter();
            if (mv.sqrMagnitude > 0.01f)
                _stateController.ChangeState(_runGetter() ? StateKey.Run : StateKey.Walk);
            if (_attackGetter())
            {
                _stateController.ChangeState(StateKey.Attack);
            }
            if (_shieldGetter())
            {
                _stateController.ChangeState(StateKey.Shield);
            }
        }

        public async UniTask Exit() { }
    }
}
