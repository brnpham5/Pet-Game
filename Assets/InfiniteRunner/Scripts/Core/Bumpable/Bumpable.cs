using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    public class Bumpable : MonoBehaviour
    {
        public delegate void BumpDelegate();
        public event BumpDelegate OnBump;

        public Meta_Layers.layers layer;

        public void Activate()
        {
            OnBump?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;

            if (obj.layer.Equals((int)layer))
            {
                Activate();
            }
        }
    }

}
