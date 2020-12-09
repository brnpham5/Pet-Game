using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    public class ChestParticle : MonoBehaviour
    {
        public ParticleSystem psOpen;
        public ParticleSystem psLand_Left;
        public ParticleSystem psLand_Right;

        public void PlayOpen()
        {
            psOpen.Play();
        }

        public void PlayLand()
        {
            psLand_Left.Play();
            psLand_Right.Play();
        }
    }

}
