using UnityEngine;
using Game.Interfaces;
using Game.Core;

namespace Game.State
{
    public class IdleState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Entering IdleState");
        }

        public void Update() { }

        public void Exit()
        {
            Debug.Log("Exiting IdleState");
        }

        public void OnSpinClicked()
        {
            var audioService = ServiceLocator.Get<IAudioService>();
            if (audioService != null) audioService.PlayClick();
            
            GameStateMachine.Instance.ChangeState<SpinningState>();
        }
    }
}
