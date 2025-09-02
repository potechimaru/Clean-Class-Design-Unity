using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.PlayerState
{
    public class RunningState : IPlayerState
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _model;
        private readonly InputService _input;
        private readonly IStateController _stateController;
        public RunningState(PlayerView view, PlayerModel model, InputService input, IStateController stateController)
        {
            _view = view;
            _model = model;
            _input = input;
            _stateController = stateController;
        }

        public async UniTask Enter()
        {
            _view.Animator?.CrossFade("Run", 0.1f);
        }

        public async UniTask Tick()
        {
            var mv = _input.MoveVec;
            if (mv.sqrMagnitude <= 0.01f) 
            { 
                _stateController.ChangeState(StateKey.Idle); return; 
            }
            if (!_input.RunHeld) 
            { 
                _stateController.ChangeState(StateKey.Walk); return; 
            }

            _view.ApplyPlanarSpeed(mv.normalized, _model.RunSpeed);
        }
        public async UniTask Exit()
        {

        }
    }
}