using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    public class BumpBoxParticles : MonoBehaviour {
        public List<ParticleSystem> hitParticles;
        public List<ParticleSystem> peakParticles;
        
        public void PlayOnHit() {
            hitParticles.ForEach(ps => {
                ps.Play();
            });
        }

        public void PlayOnPeak() {
            peakParticles.ForEach(ps => {
                ps.Play();
            });
        }
    }
}
