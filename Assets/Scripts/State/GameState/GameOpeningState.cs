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
}
