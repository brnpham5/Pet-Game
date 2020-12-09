using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    public class SpiderFlyController : MonoBehaviour
    {
        public ParticleSystem ps;
        public Animator anim;

        private void Start()
        {
            Setup();
        }

        public void Death()
        {
            ps.Play();
            anim.SetBool("IsActive", false);
        }

        public void Setup()
        {
            anim.SetBool("IsActive", true);
        }
    }

}
