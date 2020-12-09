using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    [RequireComponent(typeof(ObstacleComponent))]
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(Pusher))]
    public class Trap : MonoBehaviour
    {
        public ObstacleComponent component;
        public Damager damager;
        public Pusher pusher;

        private void Awake()
        {
            if(component == null)
            {
                component = GetComponent<ObstacleComponent>();
                Debug.Log("Forgot to reference", this);
            }

            if(damager == null)
            {
                damager = GetComponent<Damager>();
                Debug.Log("Forgot to reference", this);
            }

            if(pusher == null)
            {
                pusher = GetComponent<Pusher>();
                Debug.Log("Forgot to reference", this);
            }
        }

        public void Activate(GameObject obj)
        {
            Player player = obj.GetComponent<Player>();

            damager.DealDamage(player.damageable);
            pusher.PushDealDamage(player.rb);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;

            if (obj.CompareTag(Meta_Tags.player))
            {
                Activate(obj);
            }
        }
    }

}
