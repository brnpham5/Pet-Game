using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    public class BumpParticles : MonoBehaviour
    {
        public ParticleSystem psCoin;
        public ParticleSystem psBurstUp;
        public ParticleSystem psBurstDown;

        public void PlayParticles()
        {
            psCoin.Play();
            psBurstUp.Play();

        }

        public void PlayBumpParticles() {
            psBurstDown.Play();
        }
    }

}
