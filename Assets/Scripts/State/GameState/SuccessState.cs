using UnityEngine;
using Cysharp.Threading.Tasks;

namespace State.GameState
{
    public class SuccessState : IGameState
    {
        private readonly IStateController _controller;
        public SuccessState(IStateController controller)
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