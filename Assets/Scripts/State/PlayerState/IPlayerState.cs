using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.PlayerState
{
    public interface IPlayerState
    {
        UniTask Enter();
        UniTask Tick();
        UniTask Exit();

    }
}