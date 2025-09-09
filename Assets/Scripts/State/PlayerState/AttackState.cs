using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using UniRx;

namespace State.PlayerState
{
    public class AttackState : IPlayerState
    {
        private readonly PlayerMVCFacade _facade;
        private readonly IStateController _stateController;
        private readonly Func<Vector2> _moveGetter;
        private readonly Func<bool> _runGetter;
        private readonly Func<bool> _attackGetter;

        public AttackState(PlayerMVCFacade facade, IStateController stateController,
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
            _facade.PlayAnimation("Attack01", 0f);
            _facade.Velocity = Vector3.zero;

            await UniTask.Yield();

            var anim = _facade.Animator;
            var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            float halfTime = stateInfo.length * 0.5f;

            await UniTask.Delay(TimeSpan.FromSeconds(halfTime));

            _facade.AttackEnemies();

            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                await UniTask.Yield();

            _stateController.ChangeState(StateKey.Idle);
        }



        public async UniTask Tick()
        {
            await UniTask.CompletedTask;
        }

        public async UniTask Exit() 
        {
            // “ü—Íƒtƒ‰ƒO‚ðÁ”ï
            (_stateController as PlayerStateRunner).AttackPressed = false;
            await UniTask.CompletedTask; 
        }
    }
}
