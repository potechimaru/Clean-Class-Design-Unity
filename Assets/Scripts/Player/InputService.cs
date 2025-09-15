using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService
{
    public PlayerInputActions InputActions { get; } = new();

    public IObservable<Vector2> MoveStream { get; }
    public IObservable<bool> RunStream { get; }
    public IObservable<Unit> AttackStream { get; }
    public IObservable<bool> ShieldStream { get; }
    public IObservable<Unit> SubmitStream { get; }

    public InputService()
    {
        InputActions.Enable();

        MoveStream = Observable.EveryUpdate()
            .Select(_ => InputActions.Player.Move.ReadValue<Vector2>())
            .DistinctUntilChanged();

        RunStream = Observable.EveryUpdate()
            .Select(_ => InputActions.Player.Run.IsPressed())
            .DistinctUntilChanged();

        AttackStream = Observable.EveryUpdate()
            .Where(_ => InputActions.Player.Attack.WasPerformedThisFrame())
            .AsUnitObservable();

        ShieldStream = Observable.EveryUpdate()
            .Select(_ => InputActions.Player.Shield.IsPressed())
            .DistinctUntilChanged();

        SubmitStream = Observable.EveryUpdate()
            .Where(_ => InputActions.Player.Submit.WasPerformedThisFrame())
            .AsUnitObservable();
    }
}
