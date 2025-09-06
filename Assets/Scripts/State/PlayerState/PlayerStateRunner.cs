using System.Collections.Generic;
using VContainer.Unity;
using UnityEngine;

namespace State.PlayerState
{
    public class PlayerStateRunner : IStartable, ITickable, IStateController
    {
        private readonly Dictionary<StateKey, IPlayerState> _states = new();
        private IPlayerState _currentState;

        private readonly PlayerMVCFacade _playerMVCFacade;

        public PlayerStateRunner(PlayerMVCFacade playerMVCFacade)
        {
            _playerMVCFacade = playerMVCFacade;


            _states[StateKey.Idle] = new IdleState(_playerMVCFacade, this);
            _states[StateKey.Walk] = new WalkState(_playerMVCFacade, this);
            _states[StateKey.Run] = new RunningState(_playerMVCFacade, this);
        }

        public void Start()
        {
            Debug.Log("PlayerStateRunner Start");
            ChangeState(StateKey.Idle);
        }

        public void Tick()
        {
            _currentState?.Tick();
            _playerMVCFacade.CommitMovement(Time.deltaTime);
        }

        public void ChangeState(StateKey key)
        {
            _currentState?.Exit();
            _currentState = _states[key];
            _currentState.Enter();
        }
    }
}
