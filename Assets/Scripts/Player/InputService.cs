using UnityEngine;
using UnityEngine.InputSystem;

public class InputService
{
    public PlayerInputActions InputActions { get; } = new();

    public InputService()
    {
        InputActions.Enable();
    }

    public Vector2 MoveVec => InputActions.Player.Move.ReadValue<Vector2>();

    public bool RunHeld => InputActions.Player.Run.IsPressed();
}
