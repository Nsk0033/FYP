using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace SmallHedge.SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundsSO SO;
        private static SoundManager instance = null;
        private AudioSource audioSource;
        private Coroutine fadeCoroutine;
        private SoundType? currentBGM = null; // Track the current BGM sound type

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                audioSource = GetComponent<AudioSource>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void PlaySound(SoundType sound, AudioSource source = null, float volume = 1)
        {
            SoundList soundList = instance.SO.sounds[(int)sound];
            AudioClip[] clips = soundList.sounds;
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

            if (source)
            {
                source.outputAudioMixerGroup = soundList.mixer;
                source.clip = randomClip;
                source.volume = volume * soundList.volume;
                source.Play();
            }
            else
            {
                instance.audioSource.outputAudioMixerGroup = soundList.mixer;
                instance.audioSource.PlayOneShot(randomClip, volume * soundList.volume);
            }
        }

        public static void PlaySound(string soundName, AudioSource source = null, float volume = 1)
        {
            if (Enum.TryParse(soundName, true, out SoundType sound))
            {
                PlaySound(sound, source, volume);
            }
            else
            {
                Debug.LogWarning("SoundManager: Sound not found for name " + soundName);
            }
        }

        public static void PlayRandomBGM(SoundType soundType)
        {
            SoundList soundList = instance.SO.sounds[(int)soundType];
            AudioClip[] clips = soundList.sounds;
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
            instance.PlayBGM(randomClip, soundList.volume, soundType);
        }

        public void PlayBGM(AudioClip clip, float volume, SoundType soundType)
        {
            if (currentBGM == soundType)
            {
                return; // If the same BGM is already playing, do nothing
            }

            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeInBGM(clip, volume));
            currentBGM = soundType; // Set the current BGM
        }

        private IEnumerator FadeInBGM(AudioClip clip, float targetVolume)
        {
            if (audioSource.isPlaying)
            {
                yield return StartCoroutine(FadeOutBGM());
            }

            audioSource.clip = clip;
            audioSource.loop = true; // Set the audio source to loop
            audioSource.Play();
            audioSource.volume = 0;
            float startVolume = 0;

            while (audioSource.volume < targetVolume)
            {
                audioSource.volume += Time.deltaTime * 2;
                yield return null;
            }

            audioSource.volume = targetVolume;
        }

        private IEnumerator FadeOutBGM()
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime * 2;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
            currentBGM = null; // Reset the current BGM when it stops
        }
    }

    [Serializable]
    public struct SoundList
    {
        [HideInInspector] public string name;
        [Range(0, 1)] public float volume;
        public AudioMixerGroup mixer;
        public AudioClip[] sounds;
    }
}

