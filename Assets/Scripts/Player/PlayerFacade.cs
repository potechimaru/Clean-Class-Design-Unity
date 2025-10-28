using NUnit.Framework;
using System;
using UniRx;
using UnityEngine;

/// <summary>
/// PlayerのModelとView(Controller経由)、InputServiceへのアクセスをまとめたファサードクラス
/// </summary>
public class PlayerFacade
{
    private readonly InputService _input;
    private readonly PlayerController _controller;

    public PlayerFacade(PlayerController playerController, InputService inputService)
    {
        _controller = playerController;
        _input = inputService;
    }

    // -------------------
    // PlayerView 
    // -------------------
    public void ApplyPlanarSpeed(Vector2 input, float speed)
    {
        _controller.GetPlayerView().ApplyPlanarSpeed(input, speed);
    }

    public void CommitMovement(float deltaTime)
    {
        _controller.GetPlayerView().CommitMovement(deltaTime);
    }

    public void PlayAnimation(string stateName, float transitionDuration = 0.1f)
    {
        _controller.GetPlayerView().Animator?.CrossFade(stateName, transitionDuration);
    }

    public void AttackEnemies()
    {
        var enemies = _controller.GetPlayerView().GetEnemies();
        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(_controller.GetPlayerModel().AttackDamage);
        }
    }

    public void UpdateMoneyPossesion(int amount)
    {
        _controller.GetPlayerModel().MoneyPossession += amount;
        _controller.GetPlayerView().UpdateMoneyPossession(_controller.GetPlayerModel().MoneyPossession);
    }   

    public void ShiftShowShield(bool show)
    {
        _controller.GetPlayerView().ShiftShowShield(show);
    }

    public Animator Animator => _controller.GetPlayerView().Animator;

    public PlayerView View => _controller.GetPlayerView();


    // -------------------
    // PlayerModel
    // -------------------
    public float WalkSpeed => _controller.GetPlayerModel().WalkSpeed;
    public float RunSpeed => _controller.GetPlayerModel().RunSpeed;

    public Vector3 Velocity
    {
        get => _controller.GetPlayerModel().Velocity;
        set => _controller.GetPlayerModel().Velocity = value;
    }

    public void TakeDamage (float amount)
    {
        _controller.GetPlayerModel().TakeDamage(amount);
        _controller.GetPlayerView().UpdateHpBar();
        // Debug.Log(_model.NormalizedHp);
    }

    public void Heal(float amount)
    {
        _controller.GetPlayerModel().Heal(amount);
        _controller.GetPlayerView().UpdateHpBar();
    }

    public void PowerBuff(float amount)
    {
        _controller.GetPlayerModel().PowerBuff(amount);
    }

    public void SpeedBuff(float amount)
    {
        _controller.GetPlayerModel().SpeedBuff(amount);
    }

    public int MoneyPossession => _controller.GetPlayerModel().MoneyPossession;



    // -------------------
    // InputService
    // -------------------
    public IObservable<Vector2> MoveStream => _input.MoveStream;
    public IObservable<bool> RunStream => _input.RunStream;
    public IObservable<Unit> AttackStream => _input.AttackStream;
    public IObservable<bool> ShieldStream => _input.ShieldStream;
    public IObservable<Unit> SubmitStream => _input.SubmitStream;

}
