using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(AudioSource))]
    public class FootstepSound : MonoBehaviour
    {
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.

        private AudioSource m_AudioSource;

        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }

        public void PlayFootstepSound()
        {
            if (m_FootstepSounds.Length == 0)
            {
                Debug.LogWarning("Footstep sounds array is empty!");
                return;
            }

            // pick & play a random footstep sound from the array
            int n = UnityEngine.Random.Range(0, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }
    }
}
