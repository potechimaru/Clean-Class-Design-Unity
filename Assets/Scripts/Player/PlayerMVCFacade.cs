using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;

public class PlayerMVCFacade
{
    private readonly PlayerModel _model;
    private readonly PlayerView _view;
    private readonly InputService _input;

    public PlayerMVCFacade(PlayerModel model, PlayerView view, InputService input)
    {
        _model = model;
        _view = view;
        _input = input;
    }

    // -------------------
    // PlayerView の委譲
    // -------------------
    public void ApplyPlanarSpeed(Vector2 input, float speed)
    {
        _view.ApplyPlanarSpeed(input, speed);
    }

    public void CommitMovement(float deltaTime)
    {
        _view.CommitMovement(deltaTime);
    }

    public void PlayAnimation(string stateName, float transitionDuration = 0.1f)
    {
        _view.Animator?.CrossFade(stateName, transitionDuration);
    }

    public void AttackEnemies()
    {
        var enemies = _view.GetEnemies();
        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(_model.AttackDamage); // ダメージ値は仮
        }
    }

    public void UpdateMoneyPossesion(int amount)
    {
        _model.MoneyPossession += amount;
        _view.UpdateMoneyPossession(_model.MoneyPossession);
    }   

    public void ShiftShowShield(bool show)
    {
        _view.ShiftShowShield(show);
    }

    public Animator Animator => _view.Animator;

    public PlayerView View => _view;


    // -------------------
    // PlayerModel の委譲
    // -------------------
    public float WalkSpeed => _model.WalkSpeed;
    public float RunSpeed => _model.RunSpeed;

    public Vector3 Velocity
    {
        get => _model.Velocity;
        set => _model.Velocity = value;
    }

    public void TakeDamage (float amount)
    {
        _model.TakeDamage(amount);
        _view.UpdateHpBar();
        // Debug.Log(_model.NormalizedHp);
    }

    public int MoneyPossession => _model.MoneyPossession;



    // -------------------
    // InputServiceのストリームを公開
    // -------------------
    public IObservable<Vector2> MoveStream => _input.MoveStream;
    public IObservable<bool> RunStream => _input.RunStream;
    public IObservable<Unit> AttackStream => _input.AttackStream;
    public IObservable<bool> ShieldStream => _input.ShieldStream;
    public IObservable<Unit> SubmitStream => _input.SubmitStream;

}
