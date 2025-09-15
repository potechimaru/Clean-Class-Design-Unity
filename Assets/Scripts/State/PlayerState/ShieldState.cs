using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using UniRx;

namespace State.PlayerState
{
    public class ShieldState : IPlayerState
    {
        private readonly PlayerMVCFacade _facade;
        private readonly IStateController _stateController;
        private readonly Func<Vector2> _moveGetter;
        private readonly Func<bool> _runGetter;
        private readonly Func<bool> _shieldGetter;

        public ShieldState(PlayerMVCFacade facade, IStateController stateController,
                         Func<Vector2> moveGetter, Func<bool> runGetter, Func<bool> shieldGetter)
        {
            _facade = facade;
            _stateController = stateController;
            _moveGetter = moveGetter;
            _runGetter = runGetter;
            _shieldGetter = shieldGetter;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter Shield State");
            _facade.PlayAnimation("Defend", 0.02f);
            _facade.Velocity = Vector3.zero;
        }

        public async UniTask Tick()
        {
            if (!_shieldGetter())
            {
                _stateController.ChangeState(StateKey.Idle);
            }
        }

        public async UniTask Exit() { }
    }
}
