using UnityEngine;
using Game.State;

namespace Game.Core
{
    public class GameInitializer : MonoBehaviour
    {
        private void Start()
        {
            var stateMachine = GameStateMachine.Instance;
            if (stateMachine != null)
            {
                stateMachine.RegisterState(new IdleState());
                stateMachine.RegisterState(new PreparingSpinState());
                stateMachine.RegisterState(new SpinningState());
                stateMachine.RegisterState(new StoppingState());
                stateMachine.RegisterState(new RewardAnimationState());
                stateMachine.RegisterState(new TransitionState());
                stateMachine.RegisterState(new GameOverState());
                stateMachine.RegisterState(new InventoryState());
                // Set initial state
                stateMachine.ChangeState<IdleState>();
            }
        }
    }
}
