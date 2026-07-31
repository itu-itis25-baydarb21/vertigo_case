using UnityEngine;

namespace Game.State
{
    public class StoppingState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Entering StoppingState");
            GameStateMachine.Instance.ChangeState<RewardAnimationState>();
        }

        public void Update() { }
        public void Exit() { }
    }
}
