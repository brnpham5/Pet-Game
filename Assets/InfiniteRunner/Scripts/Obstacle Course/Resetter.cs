using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    [RequireComponent(typeof(ObstacleComponent))]
    public class Resetter : MonoBehaviour
    {
        public delegate void ResetDelegate();
        public static event ResetDelegate OnReset;

        public ObstacleComponent component;

        private void Awake()
        {
            if(component == null)
            {
                component = GetComponent<ObstacleComponent>();
                Debug.Log("Forgot to reference");
            }
        }

        public void ResetTrigger()
        {
            OnReset?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject go = collision.gameObject;

            if (go.CompareTag("Player"))
            {
                ResetTrigger();
            }
        }
    }

}
