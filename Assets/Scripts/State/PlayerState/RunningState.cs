using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.PlayerState
{
    public class RunningState : IPlayerState
    {
        private readonly PlayerMVCFacade _playerMVCFacade;
        private readonly IStateController _stateController;
        public RunningState(PlayerMVCFacade playerMVCFacade, IStateController stateController)
        {
            _stateController = stateController;
            _playerMVCFacade = playerMVCFacade;
        }

        public async UniTask Enter()
        {
            _playerMVCFacade.PlayerAnimation("Walk", 0.1f);
        }

        public async UniTask Tick()
        {
            var mv = _playerMVCFacade.MoveVec;
            if (mv.sqrMagnitude <= 0.01f) 
            { 
                _stateController.ChangeState(StateKey.Idle); return; 
            }
            if (!_playerMVCFacade.RunHeld) 
            { 
                _stateController.ChangeState(StateKey.Walk); return; 
            }

            _playerMVCFacade.ApplyPlanarSpeed(mv.normalized, _playerMVCFacade.RunSpeed);
        }
        public async UniTask Exit()
        {

        }
    }
}