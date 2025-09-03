using State.EnemyState;
using UnityEngine;

namespace State.EnemyState
{
    public interface IStateController
    {
        public void ChangeState(StateKey key);
    }
}