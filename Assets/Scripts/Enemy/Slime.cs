using Cysharp.Threading.Tasks;
using State.EnemyState;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using VContainer;

public class Slime : MonoBehaviour, IEnemy, IEnemyTick
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private IEnemyConfig _config;
    private Transform _target;
    private PlayerMVCFacade _playerMVCFacade;

    private float _hp;
    private IObjectPool<Slime> _pool; // プール参照
    [Inject] private IObjectResolver _resolver;

    [SerializeField] private float _attackRange = 1.5f;

    public EnemyStateRunner StateMachine { get; private set; }
    public EnemyView EnemyView { get; private set; }
    public bool IsDead { get; private set; }
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Log($"{name} Awake, agent={_agent}", this);

        _anim = GetComponent<Animator>();
        StateMachine = new EnemyStateRunner();


        if (_agent != null)
        {
            _agent.updateRotation = true;
            _agent.updatePosition = true;
            _agent.autoBraking = true;
        }

        EnemyView = GetComponent<EnemyView>();
    }


    /// <summary>
    /// プール側から呼ばれる：返却先を保持
    /// </summary>
    public void SetPool(IObjectPool<Slime> pool) => _pool = pool;

    public void Initialize(IEnemyConfig config, Transform target, PlayerMVCFacade playerMVCFacade)
    {
        _config = config;
        _target = target;
        _playerMVCFacade = playerMVCFacade;
        _hp = _config.MaxHp;
        IsDead = false;

        EnemyView?.Initialize(this, _config.MaxHp);

        _agent.enabled = true;
        _agent.speed = _config.MoveSpeed;
        _agent.stoppingDistance = _attackRange;
        _agent.isStopped = false;

        UpdateDestination();

        StateMachine = _resolver.Resolve<EnemyStateRunner>();
        StateMachine.AddState(StateKey.Idle, new EnemyIdleState(this, _anim, StateMachine));
        StateMachine.AddState(StateKey.Walk, new EnemyWalkState(this, _anim, StateMachine));
        StateMachine.AddState(StateKey.Attack, new EnemyAttackState(this, _anim, StateMachine));
        StateMachine.AddState(StateKey.Dead, new EnemyDeadState(this, _anim, StateMachine));
        StateMachine.AddState(StateKey.Hurt, new EnemyHurtState(this, _anim, StateMachine));


        StateMachine.ChangeState(StateKey.Walk);
    }

    public async UniTask TakeDamage(float amount)
    {
        Debug.Log($"{name} took {amount} damage.", this);
        if (IsDead) return;
        _hp -= amount;

        EnemyView?.UpdateHp(_hp);

        if (_hp <= 0f)
        {
            await Die();
        }
        else
        {
            StateMachine.ChangeState(StateKey.Hurt);
        }
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

    private async UniTask Die()
    {
        Debug.Log($"{name} is dying.", this);
        IsDead = true;

        if (_agent != null)
        {
            _agent.isStopped = true;
            _agent.ResetPath();
        }


        StateMachine.ChangeState(StateKey.Dead);

        await UniTask.Yield();

        while (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            await UniTask.Yield();

        Debug.Log("Slime died!");

        // Destroyせずプールに返却
        _pool?.Release(this);
    }

    public void HitPlayer()
    {
        _playerMVCFacade?.TakeDamage(_config.AttackDamage);
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
