using UnityEngine;
using VContainer;

public class PlayerView : MonoBehaviour
{
    [Inject] private PlayerModel _model;
    [SerializeField] private Animator _anim;

    private CharacterController _cc;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    // 状態から呼ばれる：水平方向の希望速度を渡す
    public void ApplyPlanarSpeed(Vector2 input, float speed)
    {
        // 横方向の目標速度（ワールドX Z）
        var plan = new Vector3(input.x, 0f, input.y) * speed;
        var velocity = _model.Velocity;
        velocity.x = plan.x;
        velocity.z = plan.z;
        _model.Velocity = velocity;

        if (plan.sqrMagnitude > 0.0001f)
        {
            var look = new Vector3(plan.x, 0, plan.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), 0.2f);
        }
    }

    // 毎フレームの最終移動（重力適用込み）
    public void CommitMovement(float deltaTime)
    {
        // Ground 判定は CC に任せる
        _model.IsGrounded = _cc.isGrounded;

        var velocity = _model.Velocity;

        if (_model.IsGrounded && velocity.y < 0f)
            velocity.y = -2f; // 地面に吸着

        // 重力
        velocity.y += _model.Gravity * deltaTime;

        // 実移動
        _cc.Move(velocity * deltaTime);

        _model.Velocity = velocity;

    }

    public Animator Animator => _anim;
}
