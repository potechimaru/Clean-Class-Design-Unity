using State.PlayerState;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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

    // PlayerView
    public void ApplyPlanarSpeed(Vector2 input, float speed)
    {
        _view.ApplyPlanarSpeed(input, speed);
    }

    public void PlayerAnimation(string stateName, float transitionDuration = 0.1f)
    {
        _view.Animator?.CrossFade(stateName, transitionDuration);
    }

    public void CommitMovement(float deltaTime)
    {
        _view.CommitMovement(deltaTime);
    }

    // PlayerModel
    public float WalkSpeed => _model.WalkSpeed;
    public float RunSpeed => _model.RunSpeed;
    public Vector3 Velocity
    {
        get => _model.Velocity;
        set => _model.Velocity = value;
    }

    // InputService
    public Vector2 MoveVec => _input.MoveVec;
    public bool RunHeld => _input.RunHeld;
}
