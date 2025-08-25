using UnityEngine;
using System;
using System.Collections.Generic;
using VContainer.Unity;


public class GameStateMachine : IStartable, ITickable
{
	private readonly Dictionary<Type, IGameState> _states;
	private IGameState _currentState;

	public GameStateMachine(
		GameOpeningState gameOpening,
		PlayGameState playGameState,
		SuccessState successState,
        FailedState failedState
        )
	{
		_states = new Dictionary<Type, IGameState>
		{
			[typeof(GameOpeningState)] = gameOpening,
			[typeof(PlayGameState)] = playGameState,
			[typeof(SuccessState)] = successState,
			[typeof(FailedState)] = failedState
		};

		
    }

	public void Start()
	{
		ChangeState<GameOpeningState>();
    }

	public void Tick()
	{
		_currentState?.Tick();
    }

	public void ChangeState<T>() where T : IGameState
	{
		_currentState?.Exit();
		_currentState = _states[typeof(T)];
		_currentState.Enter();
    }
}
