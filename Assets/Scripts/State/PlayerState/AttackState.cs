using Cysharp.Threading.Tasks;
using State.PlayerState;
using System;
using UnityEngine;
using static Unity.Collections.Unicode;

public class AttackState : IPlayerState
{
    private readonly PlayerMVCFacade _facade;
    private readonly IStateController _stateController;

    private int comboStep = 1;

    public AttackState(PlayerMVCFacade facade, IStateController stateController)
    {
        _facade = facade;
        _stateController = stateController;
    }

    public async UniTask Enter()
    {
        (_stateController as PlayerStateRunner).AttackPressed = false;
        string animName = comboStep switch
        {
            1 => "Attack01_event",
            2 => "Attack02_event",
            3 => "Attack03_event",
            _ => "Attack01_event"
        };

        _facade.PlayAnimation(animName, 0f);
        _facade.Velocity = Vector3.zero;

        await UniTask.Yield();

        var anim = _facade.Animator;
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            await UniTask.Yield();

        // コンボ受付があれば次へ
        if ((_stateController as PlayerStateRunner).AttackPressed && comboStep < 3)
        {
            comboStep++;
            _stateController.ChangeState(StateKey.Attack);
        }
        else
        {
            comboStep = 1; // リセット
            _stateController.ChangeState(StateKey.Idle);
        }
    }

    public async UniTask Tick() { await UniTask.CompletedTask; }
    public async UniTask Exit()
    {
        await UniTask.CompletedTask;
    }
}
