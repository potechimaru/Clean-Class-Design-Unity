using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.GameState
{
    public class PlayGameState : IGameState
    {
        private readonly IStateController _controller;
        public PlayGameState(IStateController controller)
        {
            _controller = controller;
        }

        public async UniTask Enter()
        {

        }

        public async UniTask Tick()
        {

        }

        public async UniTask Exit()
        {

        }
    }
}