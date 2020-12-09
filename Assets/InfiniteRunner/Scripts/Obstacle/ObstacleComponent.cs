using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    public class ObstacleComponent : MonoBehaviour
    {
        public delegate void ComponentDelegate();
        public event ComponentDelegate OnSetup;
        public event ComponentDelegate OnSetdown;


        public void Setup()
        {
            this.gameObject.SetActive(true);
            OnSetup?.Invoke();
        }

        public void Setdown()
        {
            this.gameObject.SetActive(false);
            OnSetdown?.Invoke();
        }

    }

}
