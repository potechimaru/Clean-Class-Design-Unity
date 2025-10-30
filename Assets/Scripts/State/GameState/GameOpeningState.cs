using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.GameState
{
    public class GameOpeningState : IGameState
    {
        private readonly IStateController _controller;
        public GameOpeningState(IStateController controller)
        {
            _controller = controller;
        }

        public async UniTask Enter()
        {
            await UniTask.CompletedTask;

            //Debug.Log("GameOpeningState: Enter");
        }

        public async UniTask Tick()
        {
            await UniTask.CompletedTask;
        }
        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
            //Debug.Log("GameOpeningState: Exit");
        }
    }
}
