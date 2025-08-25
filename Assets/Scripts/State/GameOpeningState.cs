using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameOpeningState : IGameState
{
    public GameOpeningState()
    {

    }

    public async UniTask Enter()
    {
        //Debug.Log("GameOpeningState: Enter");
    }

    public async UniTask Tick()
    {
        await UniTask.CompletedTask;
    }
    public async UniTask Exit()
    {
        //Debug.Log("GameOpeningState: Exit");
    }
}
