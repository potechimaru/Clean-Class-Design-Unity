using System.Collections.Generic;
using VContainer.Unity;

namespace State.GameState
{
    public class GameStateMachine : IStartable, ITickable, IStateController
    {
        private readonly Dictionary<StateKey, IGameState> _states = new();
        private IGameState _currentState;

        public GameStateMachine()
        {

            _states[StateKey.Opening] = new GameOpeningState(this);
            _states[StateKey.Play] = new PlayGameState(this);
            _states[StateKey.Success] = new SuccessState(this);
            _states[StateKey.Failed] = new FailedState(this);
        }

        public void Start()
        {
            ChangeState(StateKey.Play);
        }

        public void Tick()
        {
            _currentState?.Tick();
        }

        public void ChangeState(StateKey key)
        {
            _currentState?.Exit();
            _currentState = _states[key];
            _currentState.Enter();
        }
    }
}
