using UnityEngine;
using State.EnemyState;

public class Slime : MonoBehaviour, IEnemy, IEnemyTick
{
    private CharacterController _cc;
    private SlimeConfig _config;
    private Transform _target;
    private Animator _anim;

    private float _hp;
    private Vector3 _velocity;

    public EnemyStateRunner StateMachine { get; private set; }
    public bool IsDead { get; private set; }
    public Vector3 Position => transform.position;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        StateMachine = new EnemyStateRunner();
    }

    public void Initialize(SlimeConfig config, Transform target)
    {
        _config = config;
        _target = target;
        _hp = _config.maxHp;
        IsDead = false;
        _velocity = Vector3.zero;

        // State ‚ð“o˜^
        StateMachine.AddState(StateKey.Walk, new EnemyWalkState(this, _anim, StateMachine));
        StateMachine.AddState(StateKey.Attack, new EnemyAttackState(this, _anim, StateMachine));

        StateMachine.ChangeState(StateKey.Walk);
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;
        _hp -= amount;
        if (_hp <= 0f) Die();
    }

    public void Tick(float deltaTime)
    {
        if (IsDead || _config == null) return;
        StateMachine.Tick(deltaTime);
    }

    public void MoveTowardsTarget(float dt)
    {
        if (_target == null) return;

        var dir = (_target.position - transform.position);
        dir.y = 0f;

        if (dir.magnitude > 0.5f)
        {
            var planar = dir.normalized * _config.moveSpeed;
            _velocity.x = planar.x;
            _velocity.z = planar.z;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.15f);
        }
        else
        {
            _velocity.x = 0f;
            _velocity.z = 0f;
        }

        if (_cc.isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;
        _velocity.y += Physics.gravity.y * dt;
        _cc.Move(_velocity * dt);
    }

    public bool CanAttackTarget()
    {
        if (_target == null) return false;
        return Vector3.Distance(transform.position, _target.position) < 1.5f;
    }

    private void Die()
    {
        IsDead = true;
        StateMachine.ChangeState(StateKey.Dead);
        Debug.Log("Slime died!");
        Destroy(gameObject, 1f);
    }
}

