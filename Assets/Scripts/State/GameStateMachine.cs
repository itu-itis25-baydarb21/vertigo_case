using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.State
{
    public class GameStateMachine : MonoBehaviour
    {
        public static GameStateMachine Instance { get; private set; }
        
        private Dictionary<Type, IGameState> states;
        public IGameState CurrentState { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            states = new Dictionary<Type, IGameState>();
        }

        public void RegisterState(IGameState state)
        {
            Type stateType = state.GetType();
            states[stateType] = state;
        }

        public void ChangeState<T>() where T : IGameState
        {
            Type stateType = typeof(T);
            if (states.TryGetValue(stateType, out IGameState nextState))
            {
                CurrentState?.Exit();
                CurrentState = nextState;
                CurrentState.Enter();
            }
            else
            {
                Debug.LogError($"State {stateType} is not registered in GameStateMachine.");
            }
        }

        private void Update()
        {
            CurrentState?.Update();
        }
    }
}
