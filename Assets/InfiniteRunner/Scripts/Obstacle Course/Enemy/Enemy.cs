using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(EnemyBehaviour))]
    [RequireComponent(typeof(ObstacleComponent))]
    [RequireComponent(typeof(Stompable))]
    [RequireComponent(typeof(Pusher))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [Header("GameObject Reference")]
        public Collider2D cldr;
        public Damageable damageable;
        public Damager damager;
        public EnemyBehaviour behaviour;
        public ObstacleComponent obstComp;
        public Stompable stompable;
        public Pusher pusher;
        public Rigidbody2D rb;

        [Header("Child Reference")]
        public Animator anim;
        public TagTrigger damageTrigger;
        public TagTrigger stompTrigger;

        protected Vector3 initialPos;
        protected WaitForFixedUpdate wait = new WaitForFixedUpdate();
        protected WaitForSeconds deathWait = new WaitForSeconds(5.0f);
        private bool isActive;

        protected void Awake()
        {
            if(cldr == null) {
                Debug.Log("Add a collider", this);
            }

            if(damageable == null)
            {
                damageable = GetComponent<Damageable>();
            }

            if(damager == null)
            {
                damager = GetComponent<Damager>();
            }

            if(obstComp == null)
            {
                obstComp = GetComponent<ObstacleComponent>();
            }

            if(pusher == null) {
                pusher = GetComponent<Pusher>();
            }

            if(rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }

            
            initialPos = this.transform.localPosition;
        }

        private void Start() {
            obstComp.OnSetup += Setup;
            Setup();
            
        }

        public void Death() {
            SetActive(false);
            anim.SetBool("IsMoving", false);
            ChangeLayers(Meta_Layers.layers.EnemyDead);
        }

        public void Setup() {
            SetActive(true);
            behaviour.Setup();
            this.gameObject.SetActive(true);
            damageable.ResetHealth();
            this.transform.localPosition = initialPos;
            ChangeLayers(Meta_Layers.layers.Enemy);
        }

        public void SetActive(bool value)
        {
            behaviour.SetActive(value);
            isActive = value;
            stompable.SetActive(value);
            anim.SetBool("IsActive", value);
        }

        private void ChangeLayers(Meta_Layers.layers layer) {
            gameObject.layer = (int)layer;
            damageTrigger.gameObject.layer = (int)layer;
            stompTrigger.gameObject.layer = (int)layer;
        }

        public void DealDamage(GameObject obj) {
            Player player = obj.GetComponent<Player>();

            if(player != null && isActive == true)
            {
                if(player.CanBeDamaged() == true)
                {
                    damager.DealDamage(player.damageable);
                }
                
                pusher.PushDealDamage(player.rb);

                return;
            }
        }

        private void OnEnable() {
            damageable.OnDeath += Death;
            damageTrigger.OnCollide += DealDamage;
        }

        private void OnDisable() {
            damageable.OnDeath -= Death;
            damageTrigger.OnCollide -= DealDamage;
        }

        private void OnDestroy()
        {
            obstComp.OnSetup -= Setup;
        }

    }

}
