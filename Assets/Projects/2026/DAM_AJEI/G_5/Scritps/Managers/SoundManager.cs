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

        [Header("Sources")]
        public AudioSource musicSource;
        public AudioSource sfxSource;

        public AudioClip[] audioClips;

        enum SoundEffects
        {
            Music,
            Grab,
            Release,
            Move
        }

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            musicSource.outputAudioMixerGroup = musicGroup;
            sfxSource.outputAudioMixerGroup = sfxGroup;
        }

        void Start()
        {
            musicSource.clip = audioClips[(int)SoundEffects.Music];
            musicSource.loop = true;
            musicSource.Play();
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

        public void PlayRelease()
        {
            sfxSource.Stop();
            sfxSource.loop = false;
            sfxSource.PlayOneShot(audioClips[(int)SoundEffects.Release]);
            musicSource.Stop(); 
        }
    }
}