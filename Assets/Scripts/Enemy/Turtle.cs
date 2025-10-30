using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using State.EnemyState;
using Cysharp.Threading.Tasks;

public class Turtle : MonoBehaviour, IEnemy
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private IEnemyConfig _config;
    private Transform _target;
    private PlayerFacade _playerMVCFacade;
    private float _hp;
    private IObjectPool<Turtle> _pool;

    [SerializeField] private float _attackRange = 1.5f;

    public EnemyStateRunner StateMachine { get; private set; }
    public bool IsDead { get; private set; }
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

        if (_agent != null)
        {
            _agent.updateRotation = true;
            _agent.updatePosition = true;
            _agent.autoBraking = true;
        }
    }

    public void SetPool(IObjectPool<Turtle> pool) => _pool = pool;

    /// <summary>
    /// EnemyManagerÇ©ÇÁåƒÇŒÇÍÇÈèâä˙âª
    /// </summary>
    public void Initialize(IEnemyConfig config, Transform target, PlayerFacade playerMVCFacade, EnemyStateRunner stateMachine)
    {
        _config = config;
        _target = target;
        _playerMVCFacade = playerMVCFacade;
        _hp = _config.MaxHp;
        IsDead = false;
        StateMachine = stateMachine;

        _agent.enabled = true;
        _agent.speed = _config.MoveSpeed;
        _agent.stoppingDistance = _attackRange;
        _agent.isStopped = false;

        UpdateDestination();

        StateMachine.ChangeState(StateKey.Idle);
    }

    public async UniTask TakeDamage(float amount)
    {
        if (IsDead) return;

        _hp -= amount;
        if (_hp <= 0f)
            await Die();
        else
            StateMachine.ChangeState(StateKey.Hurt);
    }

    public void Tick(float deltaTime)
    {
        if (IsDead || _config == null) return;

        if (_target && _agent && !_agent.isStopped)
            UpdateDestination();

        StateMachine?.Tick(deltaTime);
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
            _agent.SetDestination(_target.position);
    }

    private async UniTask Die()
    {
        IsDead = true;

        if (_agent != null)
        {
            _agent.isStopped = true;
            _agent.ResetPath();
        }

        StateMachine.ChangeState(StateKey.Dead);

        await UniTask.WaitUntil(() =>
            _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        _pool?.Release(this);
    }

    private void OnDisable()
    {
        if (_agent != null)
        {
            _agent.ResetPath();
            _agent.isStopped = true;
        }
    }
}
