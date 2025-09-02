using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.PlayerState
{
    public class IdleState : IPlayerState
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _model;
        private readonly InputService _input;
        private readonly IStateController _stateController;
        public IdleState(PlayerView view, PlayerModel model, InputService input, IStateController stateController)
        {
            _view = view;
            _model = model;
            _input = input;
            _stateController = stateController;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter Idle State");
        }

        public async UniTask Tick()
        {
            Debug.Log("Idle Tick");
            var mv = _input.MoveVec;
            if (mv.sqrMagnitude > 0.01f)
                _stateController.ChangeState(_input.RunHeld ? StateKey.Run : StateKey.Walk);
        }

        public async UniTask Exit()
        {

        }
    }
}