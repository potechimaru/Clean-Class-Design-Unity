using State.PlayerState;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerController : ITickable
{
    private readonly PlayerView _view;

    public PlayerController(PlayerView view)
    {
        _view = view;
    }

    public void Tick()
    {
        _view.CommitMovement(Time.deltaTime);
    }
}
