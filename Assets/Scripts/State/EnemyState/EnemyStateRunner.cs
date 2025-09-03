using System;
using System.Collections.Generic;
using UnityEngine;

namespace State.EnemyState
{

    public class EnemyStateRunner: IStateController
    {
        private readonly Dictionary<StateKey, IEnemyState> _states = new();
        private IEnemyState _currentState;

        public void AddState(StateKey key, IEnemyState state)
        {
            if (!_states.ContainsKey(key))
                _states[key] = state;
        }

        public void ChangeState(StateKey key)
        {
            _currentState?.Exit();
            _currentState = _states[key];
            _currentState.Enter();
        }

        public void Tick(float dt)
        {
            _currentState?.Tick(dt);
        }
    }
}
