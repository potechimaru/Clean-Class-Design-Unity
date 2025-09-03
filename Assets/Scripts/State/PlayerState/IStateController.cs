using State.PlayerState;
using UnityEngine;

namespace State.PlayerState
{
    public interface IStateController
    {
        public void ChangeState(StateKey key);
    }
}