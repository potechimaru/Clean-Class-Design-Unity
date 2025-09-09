using System.Collections.Generic;
using VContainer.Unity;
using UnityEngine;
using UniRx;

namespace State.PlayerState
{
    public class PlayerStateRunner : IStartable, ITickable, IStateController
    {
        private readonly Dictionary<StateKey, IPlayerState> _states = new();
        private IPlayerState _currentState;
        private readonly PlayerMVCFacade _facade;

        private Vector2 _moveInput;
        private bool _runHeld;
        public bool AttackPressed { get; set; }

        public PlayerStateRunner(PlayerMVCFacade facade)
        {
            _facade = facade;

            // “ü—Íw“Ç
            _facade.MoveStream.Subscribe(mv => _moveInput = mv).AddTo(_facade.View);
            _facade.RunStream.Subscribe(run => _runHeld = run).AddTo(_facade.View);
            _facade.AttackStream.Subscribe(_ => AttackPressed = true).AddTo(_facade.View);

            // ó‘Ô‚ð“o˜^
            _states[StateKey.Idle] = new IdleState(_facade, this, () => _moveInput, () => _runHeld, () => AttackPressed);
            _states[StateKey.Walk] = new WalkState(_facade, this, () => _moveInput, () => _runHeld, () => AttackPressed);
            _states[StateKey.Run] = new RunningState(_facade, this, () => _moveInput, () => _runHeld, () => AttackPressed);
            _states[StateKey.Attack] = new AttackState(_facade, this, () => _moveInput, () => _runHeld, () => AttackPressed);
        }

        public void Start()
        {
            Debug.Log("PlayerStateRunner Start");
            ChangeState(StateKey.Idle);
        }

        public void Tick()
        {
            _currentState?.Tick();
            _facade.CommitMovement(Time.deltaTime);
        }

        public void ChangeState(StateKey key)
        {
            _currentState?.Exit();
            _currentState = _states[key];
            _currentState.Enter();
        }
    }
}
