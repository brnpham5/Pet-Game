using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(AudioSource))]
    public class SFXManager : MonoBehaviour
    {
        [Header("Scriptable Reference")]
        public SFXSet set;

        [Header("GameObject Reference")]
        public AudioSource audioSource;

        [Header("Configuration")]
        public bool pitchShift = false;
        [Range(0.0f, 0.5f)]
        public float pitchRange;

        private float pitchBase;

        private void Awake()
        {
            if(audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            pitchBase = audioSource.pitch;
        }

        public void Play()
        {
            int index = Random.Range(0, set.clips.Count);

            audioSource.clip = set.clips[index];

            if(pitchShift == true)
            {
                PitchShift();
            }
            audioSource.Play();
        }

        public void PitchShift()
        {
            audioSource.pitch = Random.Range(pitchBase - pitchRange, pitchBase + pitchRange);
        }

        private void OnEnable()
        {

            set.OnPlay += Play;
        }

        private void OnDisable()
        {

            set.OnPlay -= Play;
        }
    }
}
