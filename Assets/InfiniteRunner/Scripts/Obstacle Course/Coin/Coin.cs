using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace InfiniteRunner
{
    [RequireComponent(typeof(ObstacleComponent))]
    [RequireComponent(typeof(Pickuppable))]
    public class Coin : MonoBehaviour
    {
        [Header("Scriptable Reference")]
        public SFXSet sfxCoin;

        [Header("GameObject Reference")]
        public ObstacleComponent component;
        public Pickuppable pickuppable;

        private void Awake()
        {
            if (component == null)
            {
                component = GetComponent<ObstacleComponent>();
                Debug.Log("Forgot to reference", this);
            }

            if(pickuppable == null)
            {
                pickuppable = GetComponent<Pickuppable>();
                Debug.Log("Forgot to reference", this);
            }
        }

        public void Activate()
        {
            pickuppable.Activate();
            component.Setdown();
            sfxCoin.Play();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;

            if (obj.CompareTag(Meta_Tags.player))
            {
                Activate();
            }
        }
    }
}
