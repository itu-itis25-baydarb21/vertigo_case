namespace Game.Interfaces
{
    public interface IAudioService
    {
        void StartSpinSound();
        void StopSpinSound();
        void PlayClick();
        void PlayWin();
        void PlayBomb();
        void PlayTick();
    }
}
