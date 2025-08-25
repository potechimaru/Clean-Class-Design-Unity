using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IGameState
{
    UniTask Enter();
    UniTask Tick();
    UniTask Exit();

}
