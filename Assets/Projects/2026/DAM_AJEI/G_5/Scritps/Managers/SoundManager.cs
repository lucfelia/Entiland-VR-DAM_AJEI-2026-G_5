using UnityEngine;
using UnityEngine.Audio;

namespace Autohand.Demo
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance { get; private set; }

        [Header("Mixer Groups")]
        public AudioMixerGroup musicGroup;
        public AudioMixerGroup sfxGroup;
        public AudioMixerGroup ambientalGroup;

        [Header("Sources")]
        public AudioSource ambiental;
        public AudioSource musicSource;
        public AudioSource sfxSource;

        public AudioClip[] audioClips;

        enum SoundEffects
        {
            Music,
            Grab,
            Release,
            Move, 
            Ambiental
        }

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            musicSource.outputAudioMixerGroup = musicGroup;
            sfxSource.outputAudioMixerGroup = sfxGroup;
            ambiental.outputAudioMixerGroup = ambientalGroup;

        }

        void Start()
        {
            musicSource.clip = audioClips[(int)SoundEffects.Music];
            musicSource.loop = true;
            ambiental.clip = audioClips[(int)SoundEffects.Ambiental];
            ambiental.loop = true;
            ambiental.Play();
        }

        public void PlayGrab()
        {
            sfxSource.Stop();
            sfxSource.PlayOneShot(audioClips[(int)SoundEffects.Grab]);
        }

        public void PlayMove()
        {
            sfxSource.Stop();
            sfxSource.clip = audioClips[(int)SoundEffects.Move];
            sfxSource.loop = true;
            sfxSource.Play();
        }
        public void StopMove()
        {
            if (sfxSource.clip == audioClips[(int)SoundEffects.Move])
                sfxSource.Stop();
        }
        public void PlayMusic()
        {
            if (!musicSource.isPlaying)
                musicSource.Play();
        }
        public void PlayRelease()
        {
            sfxSource.Stop();
            sfxSource.loop = false;
            sfxSource.PlayOneShot(audioClips[(int)SoundEffects.Release]);
            musicSource.Stop(); 
        }
    }
}