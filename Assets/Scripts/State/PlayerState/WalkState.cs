using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UniRx;

namespace State.PlayerState
{
    public class WalkState : IPlayerState
    {
        private readonly PlayerFacade _facade;
        private readonly IStateController _stateController;
        private readonly Func<Vector2> _moveGetter;
        private readonly Func<bool> _runGetter;
        private readonly Func<bool> _attackGetter;
        private readonly Func<bool> _shieldGetter;

        public WalkState(PlayerFacade facade, IStateController stateController,
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
            _facade.PlayAnimation("Walk01", 0.1f);
        }

        public async UniTask Tick()
        {
            var mv = _moveGetter();
            if (mv.sqrMagnitude <= 0.01f)
            {
                _stateController.ChangeState(StateKey.Idle);
                return;
            }
            if (_runGetter())
            {
                _stateController.ChangeState(StateKey.Run);
                return;
            }
            _facade.ApplyPlanarSpeed(mv.normalized, _facade.WalkSpeed);
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
