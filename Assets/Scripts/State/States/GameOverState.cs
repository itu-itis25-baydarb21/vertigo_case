using UnityEngine;
using Game.UI;
using Game.Interfaces;
using Game.Core;

namespace Game.State
{
    public class GameOverState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Entering GameOverState");
            var economyController = Object.FindObjectOfType<EconomyController>();
            if (economyController != null)
            {
                economyController.ShowBombPopup();
            }
            var audioService = ServiceLocator.Get<IAudioService>();
            if (audioService != null)
            {
                audioService.PlayBomb();
            }
        }

        public void Update() { }

        public void Exit()
        {
            var economyController = Object.FindObjectOfType<EconomyController>();
            if (economyController != null)
            {
                economyController.HideBombPopup();
            }
        }
    }
}
