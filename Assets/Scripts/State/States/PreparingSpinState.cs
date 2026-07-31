using UnityEngine;

namespace Game.State
{
    public class PreparingSpinState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Entering PreparingSpinState");
            GameStateMachine.Instance.ChangeState<SpinningState>();
        }

        public void Update() { }
        public void Exit() { }
    }
}
