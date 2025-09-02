using System.Collections.Generic;
using VContainer.Unity;
using UnityEngine;

namespace State.PlayerState
{
    public class PlayerStateRunner : IStartable, ITickable, IStateController
    {
        private readonly Dictionary<StateKey, IPlayerState> _states = new();
        private IPlayerState _currentState;

        private readonly PlayerView _view;
        private readonly PlayerModel _model;
        private readonly InputService _input;

        public PlayerStateRunner(PlayerView view, PlayerModel model, InputService input)
        {
            _view = view;
            _model = model;
            _input = input;

            _states[StateKey.Idle] = new IdleState(_view, _model, _input, this);
            _states[StateKey.Walk] = new WalkState(_view, _model, _input, this);
            _states[StateKey.Run] = new RunningState(_view, _model, _input, this);
        }

        public void Start()
        {
            //Debug.Log("PlayerStateRunner Start");
            ChangeState(StateKey.Idle);
        }

        public void Tick()
        {
            _currentState?.Tick();
            _view.CommitMovement(Time.deltaTime);
        }

        public void ChangeState(StateKey key)
        {
            _currentState?.Exit();
            _currentState = _states[key];
            _currentState.Enter();
        }
    }
}
