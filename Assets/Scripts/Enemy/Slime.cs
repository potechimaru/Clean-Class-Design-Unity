using Cysharp.Threading.Tasks;
using State.EnemyState;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using VContainer;

public class Slime : MonoBehaviour, IEnemy
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private IEnemyConfig _config;
    private Transform _target;
    private PlayerFacade _playerMVCFacade;
    private float _hp;
    private IObjectPool<Slime> _pool;

    [Inject] private IObjectResolver _resolver;
    [Inject] private CoinFactory _coinFactory;

    [SerializeField] private float _attackRange = 1.5f;

    public EnemyStateRunner StateMachine { get; private set; }
    public EnemyView EnemyView { get; private set; }
    public bool IsDead { get; private set; }
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        EnemyView = GetComponent<EnemyView>();

        if (_agent != null)
        {
            _agent.updateRotation = true;
            _agent.updatePosition = true;
            _agent.autoBraking = true;
        }
    }

    public void SetPool(IObjectPool<Slime> pool) => _pool = pool;

    public void Initialize(
        IEnemyConfig config,
        Transform target,
        PlayerFacade playerMVCFacade,
        EnemyStateRunner stateMachine)
    {
        _config = config;
        _target = target;
        _playerMVCFacade = playerMVCFacade;
        _hp = _config.MaxHp;
        IsDead = false;
        StateMachine = stateMachine;

        if (EnemyView != null)
        {
            _resolver.Inject(EnemyView);
            EnemyView.Initialize(this, _config.MaxHp);
        }

        _agent.enabled = true;
        _agent.speed = _config.MoveSpeed;
        _agent.stoppingDistance = _attackRange;
        _agent.isStopped = false;

        UpdateDestination();

        // èâä˙Stateê›íË
        StateMachine.ChangeState(StateKey.Walk);
    }

    public async UniTask TakeDamage(float amount)
    {
        if (IsDead) return;
        _hp -= amount;

        EnemyView?.UpdateHp(_hp);
        EnemyView?.ShowDamage((int)amount, transform.position);

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
        _agent.isStopped = true;
        _agent.ResetPath();

        StateMachine.ChangeState(StateKey.Dead);

        await UniTask.WaitUntil(() =>
            _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        _coinFactory.Create(transform.position, _config.DropMoney);
        _pool?.Release(this);
    }

    public void HitPlayer()
        => _playerMVCFacade?.TakeDamage(_config.AttackDamage);

    private void OnDisable()
    {
        if (_agent != null)
        {
            _agent.ResetPath();
            _agent.isStopped = true;
        }
    }
}
