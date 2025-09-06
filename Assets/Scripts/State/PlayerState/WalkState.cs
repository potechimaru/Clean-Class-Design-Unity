using Cysharp.Threading.Tasks;
using State.GameState;
using UnityEngine;

namespace State.PlayerState
{
    public class WalkState : IPlayerState
    {
        private readonly PlayerMVCFacade _playerMVCFacade;
        private readonly IStateController _stateController;
        public WalkState(PlayerMVCFacade playerMVCFacade,IStateController stateController)
        {
            _stateController = stateController;
            _playerMVCFacade = playerMVCFacade;
        }

        public async UniTask Enter() 
        { 
            //Debug.Log("Enter Walk State");
            _playerMVCFacade.PlayerAnimation("Walk", 0.1f);
        }

        public async UniTask Tick()
        {
            var mv = _playerMVCFacade.MoveVec;
            if (mv.sqrMagnitude <= 0.01f) 
            { 
                _stateController.ChangeState(StateKey.Idle); 
                return; 
            }
            if (_playerMVCFacade.RunHeld) 
            { 
                _stateController.ChangeState(StateKey.Run);
                return; 
            }

            _playerMVCFacade.ApplyPlanarSpeed(mv.normalized, _playerMVCFacade.WalkSpeed);
            //_view.CommitMovement(Time.deltaTime);
        }
        public async UniTask Exit()
        {

        }
    }
}