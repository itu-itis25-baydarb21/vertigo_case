using UnityEngine;

namespace Game.State
{
    public class InventoryState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Entering InventoryState");
        }

        public void Update() { }
        public void Exit() { }
    }
}
