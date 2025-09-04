using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using State.EnemyState;

public class Slime : MonoBehaviour, IEnemy, IEnemyTick
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private SlimeConfig _config;
    private Transform _target;

    private float _hp;
    private IObjectPool<Slime> _pool; // プール参照

    [SerializeField] private float _attackRange = 1.5f;

    public EnemyStateRunner StateMachine { get; private set; }
    public bool IsDead { get; private set; }
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        StateMachine = new EnemyStateRunner();

        _agent.updateRotation = true;
        _agent.updatePosition = true;
        _agent.autoBraking = true;
    }

    /// <summary>
    /// プール側から呼ばれる：返却先を保持
    /// </summary>
    public void SetPool(IObjectPool<Slime> pool) => _pool = pool;

    public void Initialize(SlimeConfig config, Transform target)
    {
        _config = config;
        _target = target;
        _hp = _config.maxHp;
        IsDead = false;

        _agent.enabled = true;
        _agent.speed = _config.moveSpeed;
        _agent.stoppingDistance = _attackRange;
        _agent.isStopped = false;

        UpdateDestination();

        StateMachine.AddState(StateKey.Walk, new EnemyWalkState(this, _anim, StateMachine));
        StateMachine.AddState(StateKey.Attack, new EnemyAttackState(this, _anim, StateMachine));
        StateMachine.AddState(StateKey.Dead, new EnemyDeadState(this, _anim, StateMachine));
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

        if (_target && _agent && !_agent.isStopped)
        {
            UpdateDestination();
        }

        StateMachine.Tick(deltaTime);
    }

    public void MoveToTarget(float _) => UpdateDestination();

    public bool CanAttackTarget()
    {
        if (_target == null || _agent == null) return false;
        if (_agent.pathPending) return false;

        return _agent.remainingDistance <= _agent.stoppingDistance;
    }

    private void UpdateDestination()
    {
        if (_agent != null && _agent.enabled && _target != null)
        {
            _agent.SetDestination(_target.position);
        }
    }

    private void Die()
    {
        IsDead = true;

        if (_agent != null)
        {
            _agent.isStopped = true;
            _agent.ResetPath();
        }

        StateMachine.ChangeState(StateKey.Dead);

        Debug.Log("Slime died!");

        // Destroyせずプールに返却
        _pool?.Release(this);
    }

    // プール返却時に呼ばれる
    private void OnDisable()
    {
        if (_agent != null)
        {
            _agent.ResetPath();
            _agent.isStopped = true;
        }
    }
}
