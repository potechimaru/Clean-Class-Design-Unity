using System.Collections.Generic;
using UnityEngine;
using VContainer;
using TMPro;

public class PlayerView : MonoBehaviour
{
    [Inject] private SlashEffectFactory _slashEffectFactory;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _attackRange = 2f;     // ‘O•û‹——£
    [SerializeField] private float _attackRadius = 1f;    // ‹…‚Ì”¼Œa
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private HPBar _hpBar;
    [SerializeField] private TextMeshProUGUI _moneyUI;
    [SerializeField] private GameObject _shieldObject;

    private PlayerController _controller;

    private CharacterController _cc;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    // ó‘Ô‚©‚çŒÄ‚Î‚ê‚éF…•½•ûŒü‚ÌŠó–]‘¬“x‚ð“n‚·
    // PlayerView.cs
    public void ApplyPlanarSpeed(Vector2 input, float speed)
    {
        var plan = new Vector3(input.x, 0f, input.y) * speed;
        var velocity = _controller.GetPlayerModel().Velocity;
        velocity.x = plan.x;
        velocity.z = plan.z;
        _controller.GetPlayerModel().Velocity = velocity;

        if (plan.sqrMagnitude > 0.0001f)
        {
            var look = new Vector3(plan.x, 0, plan.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), 0.2f);
        }
    }

    public void CommitMovement(float deltaTime)
    {
        _controller.GetPlayerModel().IsGrounded = _cc.isGrounded;

        var velocity = _controller.GetPlayerModel().Velocity;
        if (_controller.GetPlayerModel().IsGrounded && velocity.y < 0f)
            velocity.y = -2f;
        velocity.y += _controller.GetPlayerModel().Gravity * deltaTime;
        _cc.Move(velocity * deltaTime);
        _controller.GetPlayerModel().Velocity = velocity;
    }


    /// <summary>
    /// ‘O•û‚Ì‹…”ÍˆÍ‚É‚¢‚éIEnemy‚ðŽæ“¾
    /// </summary>
    public List<IEnemy> GetEnemies()
    {
        Vector3 center = transform.position + transform.forward * _attackRange;
        center.y += 0.5f;
        Collider[] hits = Physics.OverlapSphere(center, _attackRadius, _enemyLayer);

        var enemies = new List<IEnemy>();
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IEnemy>(out var enemy))
            {
                enemies.Add(enemy);
            }
        }

        return enemies;
    }

    public void AttackEnemies()
    {
        var enemies = GetEnemies();
        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(_controller.GetPlayerModel().AttackDamage);
        }

        Vector3 effectPos = transform.position + transform.forward * _attackRange * 0.5f;
        Quaternion effectRot = Quaternion.LookRotation(transform.forward);
        _slashEffectFactory.Create(effectPos, effectRot);
    }

    public void UpdateHpBar()
    {
        if (_hpBar != null)
        {
            //Debug.Log(_model.NormalizedHp);
            _hpBar.SetFill(_controller.GetPlayerModel().NormalizedHp);
        }
    }

    public void UpdateMoneyPossession(int amount)
    {

        _moneyUI.text = _controller.GetPlayerModel().MoneyPossession.ToString("N0");
    }

    public void ShiftShowShield(bool show)
    {
        if (_shieldObject != null)
        {
            _shieldObject.SetActive(show);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = transform.position + transform.forward * _attackRange;
        center.y += 0.5f;
        Gizmos.DrawWireSphere(center, _attackRadius);
    }


    public Animator Animator => _anim;

    public void SetPlayerController(PlayerController controller)
    {
        _controller = controller;
    }
}
