using State.PlayerState;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerController : ITickable
{
    private readonly PlayerModel _model;
    private readonly PlayerView _view;
    private readonly InputService _input;
    private readonly IStateController _sm;

    public PlayerController(PlayerModel model, PlayerView view, InputService input, IStateController sm)
    {
        _model = model;
        _view = view;
        _input = input;
        _sm = sm;
    }

    public void Tick()
    {
        _view.CommitMovement(Time.deltaTime);
    }
}
