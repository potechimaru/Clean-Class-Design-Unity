using UnityEngine;

namespace State.PlayerState
{
    public interface IStateController
    {
        void ChangeState(StateKey key);
    }
}