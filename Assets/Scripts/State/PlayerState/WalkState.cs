using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UniRx;

namespace State.PlayerState
{
    public class WalkState : IPlayerState
    {
        private readonly PlayerMVCFacade _facade;
        private readonly IStateController _stateController;
        private readonly Func<Vector2> _moveGetter;
        private readonly Func<bool> _runGetter;
        private readonly Func<bool> _attackGetter;

        public WalkState(PlayerMVCFacade facade, IStateController stateController,
                         Func<Vector2> moveGetter, Func<bool> runGetter, Func<bool> attackGetter)
        {
            _facade = facade;
            _stateController = stateController;
            _moveGetter = moveGetter;
            _runGetter = runGetter;
            _attackGetter = attackGetter;
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
        }

        public async UniTask Exit() { }
    }
}
