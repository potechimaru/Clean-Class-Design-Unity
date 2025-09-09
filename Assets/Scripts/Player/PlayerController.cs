using VContainer.Unity;
using UnityEngine;

public class PlayerController : ITickable
{
    private readonly PlayerView _view;
    private readonly PlayerModel _model;

    public Vector2 MoveInput { get; set; }
    public bool RunHeld { get; set; }

    public PlayerController(PlayerView view, PlayerModel model)
    {
        _view = view;
        _model = model;
    }

    public void Tick()
    {
        float speed = RunHeld ? _model.RunSpeed : _model.WalkSpeed;
        _view.ApplyPlanarSpeed(MoveInput, speed);
        _view.CommitMovement(Time.deltaTime);
    }
}
