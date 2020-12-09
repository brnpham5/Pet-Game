using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    [RequireComponent(typeof(ObstacleComponent))]
    [RequireComponent(typeof(Rewardable))]
    [RequireComponent(typeof(Unlockable))]
    public class Chest_Sml : MonoBehaviour
    {

        [Header("GameObject Reference")]
        public ObstacleComponent component;
        public Rewardable rewardable;
        public Unlockable unlockable;
        public Animator anim;

        private bool isOpen;

        private void Awake()
        {
            if (component == null)
            {
                component = GetComponent<ObstacleComponent>();
                Debug.Log("Forgot to reference", this);
            }

            if(rewardable == null)
            {
                rewardable = GetComponent<Rewardable>();
                Debug.Log("Forgot to reference", this);
            }

            if (unlockable == null)
            {
                unlockable = GetComponent<Unlockable>();
                Debug.Log("Forgot to reference", this);
            }

            component.OnSetup += Setup;
        }

        public void Setup()
        {
            isOpen = false;
            anim.SetBool("IsOpen", false);
        }

        public void Activate()
        {
            if (!isOpen)
            {
                unlockable.Unlock();
            }
            
        }

        

        public void Open()
        {
            isOpen = true;

            //open animation
            anim.SetBool("IsOpen", true);

            //grant reward
            rewardable.Grant();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;

            if (obj.CompareTag(Meta_Tags.player))
            {
                Activate();
            }
        }

        private void OnEnable()
        {
            unlockable.OnUnlock += Open;
        }

        private void OnDisable()
        {
            unlockable.OnUnlock -= Open;
        }

        private void OnDestroy()
        {
            component.OnSetup -= Setup;
        }


    }

}
