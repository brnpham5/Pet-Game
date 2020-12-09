using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    [RequireComponent(typeof(ObstacleComponent))]
    [RequireComponent(typeof(Pusher))]
    public class Spring : MonoBehaviour
    {
        [Header("GameObject Reference")]
        public ObstacleComponent component;
        public Pusher pusher;

        public Animator anim;

        private void Awake()
        {
            if (!gameObject.tag.Equals(Meta_Tags.interactable))
            {
                gameObject.tag = Meta_Tags.interactable;
                Debug.Log("Forgot to tag", this);
            }

            if(component == null)
            {
                component = GetComponent<ObstacleComponent>();
                Debug.Log("Forgot to reference", this);
            }

            component.OnSetup += Setup;
        }

        public void Activate(GameObject obj)
        {
            anim.SetBool("IsReady", false);

            Player player = obj.GetComponent<Player>();
            pusher.PushDealDamage(player.rb);
        }

        public void Setup()
        {
            anim.SetBool("IsReady", true);
        }

        public void Setdown()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;

            if (obj.CompareTag(Meta_Tags.player))
            {
                Activate(obj);
            }
        }

        private void OnDestroy()
        {
            component.OnSetup -= Setup;
        }
    }

}
