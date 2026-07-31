using UnityEngine;
using Game.Core;
using Game.Interfaces;

namespace Game.State
{
    public class TransitionState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Entering TransitionState");
            var zoneService = ServiceLocator.Get<IZoneService>();
            if (zoneService != null)
            {
                zoneService.MoveToNextZone();
            }
            GameStateMachine.Instance.ChangeState<IdleState>();
        }

        public void Update() { }
        public void Exit() { }
    }
}
