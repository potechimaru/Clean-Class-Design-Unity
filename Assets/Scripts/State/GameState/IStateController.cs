using State.PlayerState;
using UnityEngine;

namespace State.GameState
{
    public interface IStateController
    {
        public void ChangeState(StateKey key);
    }
}