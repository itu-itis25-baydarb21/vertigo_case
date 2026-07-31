using UnityEngine;
using Game.Interfaces;
using Game.Core;

namespace Game.Audio
{
    public class AudioManager : MonoBehaviour, IAudioService
    {
        [Header("Audio Sources")]
        public AudioSource sfxSource; 
        public AudioSource bgmSource; 
        public AudioSource spinSource;

        [Header("Audio Clips")]
        public AudioClip spinTickClip;
        public AudioClip winClip;
        public AudioClip bombClip;
        public AudioClip buttonClickClip;

        private void Awake()
        {
            ServiceLocator.Register<IAudioService>(this);
        }

        public void StartSpinSound()
        {
            if (spinTickClip != null && spinSource != null)
            {
                spinSource.clip = spinTickClip;
                spinSource.loop = true;
                spinSource.Play();
            }
        }

        public void StopSpinSound()
        {
            if (spinSource != null)
            {
                spinSource.loop = false; 
                spinSource.Stop(); 
            }
        }

        public void PlaySFX(AudioClip clip)
        {
            if (clip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        public void PlayClick() => PlaySFX(buttonClickClip);
        public void PlayWin() => PlaySFX(winClip);
        public void PlayBomb() => PlaySFX(bombClip);

        public void PlayTick()
        {
            if (spinTickClip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(spinTickClip, 0.5f);
            }
        }
    }
}