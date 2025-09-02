using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.GameState
{
    public interface IGameState
    {
        UniTask Enter();
        UniTask Tick();
        UniTask Exit();

    }
}