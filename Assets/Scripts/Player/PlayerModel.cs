using UnityEngine;

public class PlayerModel
{
    public float WalkSpeed { get; } = 3.5f;
    public float RunSpeed { get; } = 6.5f;
    public float Gravity { get; } = -18f;
    public float GroundSnapDistance { get; } = 0.2f;

    public bool IsGrounded { get; set; } = true;
    public Vector3 Velocity { get; set; } = Vector3.zero; // y¬•ª‚Éd—Í‚ğ“ü‚ê‚é

    public LayerMask GroundLayer { get; set; } = LayerMask.GetMask("Ground");
}
