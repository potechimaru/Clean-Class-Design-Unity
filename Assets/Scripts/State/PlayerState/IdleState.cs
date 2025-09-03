using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.PlayerState
{
    public class IdleState : IPlayerState
    {
        private readonly PlayerMVCFacade _playerMVCFacade;
        private readonly IStateController _stateController;
        public IdleState(PlayerMVCFacade playerMVCFacade, IStateController stateController)
        {
            _stateController = stateController;
            _playerMVCFacade = playerMVCFacade;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter Idle State");
            _playerMVCFacade.PlayerAnimation("Idle", 0.1f);
            _playerMVCFacade.Velocity = Vector3.zero;
        }

        public async UniTask Tick()
        {
            Debug.Log("Idle Tick");
            var mv = _playerMVCFacade.MoveVec;
            if (mv.sqrMagnitude > 0.01f)
                _stateController.ChangeState(_playerMVCFacade.RunHeld ? StateKey.Run : StateKey.Walk);
        }

        public async UniTask Exit()
        {

        }
    }
}