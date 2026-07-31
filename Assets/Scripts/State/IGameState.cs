namespace Game.State
{
    public interface IGameState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
