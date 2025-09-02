using Cysharp.Threading.Tasks;
using State.GameState;
using UnityEngine;

namespace State.PlayerState
{
    public class WalkState : IPlayerState
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _model;
        private readonly InputService _input;
        private readonly IStateController _stateController;
        public WalkState(PlayerView view, PlayerModel model, InputService input, IStateController stateController)
        {
            _view = view;
            _model = model;
            _input = input;
            _stateController = stateController;
        }

        public async UniTask Enter() 
        { 
            Debug.Log("Enter Walk State");
            _view.Animator?.CrossFade("Walk", 0.1f); 
        }

        public async UniTask Tick()
        {
            var mv = _input.MoveVec;
            if (mv.sqrMagnitude <= 0.01f) 
            { 
                _stateController.ChangeState(StateKey.Idle); 
                return; 
            }
            if (_input.RunHeld) 
            { 
                _stateController.ChangeState(StateKey.Run);
                return; 
            }

            _view.ApplyPlanarSpeed(mv.normalized, _model.WalkSpeed);
            //_view.CommitMovement(Time.deltaTime);
        }
        public async UniTask Exit()
        {

        }
    }
}