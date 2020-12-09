using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(ObstacleComponent))]
    [RequireComponent(typeof(Pusher))]
    public class Kickable : MonoBehaviour {
        public Damageable damageable;
        public Damager damager;
        public Movable movable;
        public ObstacleComponent obstComp;
        public Pusher pusher;
        public TagTrigger enemyTrigger;
        public TagTrigger kickTrigger;

        [Header("Configuration")]
        public Vector2 kickJump = new Vector2(6f, 5f);
        public Vector2 deathForce = new Vector2(6f, 3f);
        public float kickSpeed = 10.0f;
        public float kickAcceleration = 12.0f;
        public float kickDuration = 5.0f;
        public Vector2 kickForce;

        private bool isMoving = false;
        private bool canHitEnemy = false;
        private bool canBeKicked = false;
        private float kickTimer = 0.0f;
        private WaitForFixedUpdate wait = new WaitForFixedUpdate();
        private WaitForSeconds waitDeath = new WaitForSeconds(0.35f);

        private void Awake() {
            if(damageable == null)
            {
                damageable = GetComponent<Damageable>();
            }

            if(movable == null) {
                movable = GetComponent<Movable>();
            }

            if(obstComp == null)
            {
                obstComp = GetComponent<ObstacleComponent>();
            }

            if(pusher == null){
                pusher = GetComponent<Pusher>();
            }

            obstComp.OnSetup += Setup;
        }

        public void Death()
        {
            StartCoroutine(DeathCoroutine());
        }

        public void Kick(GameObject obj) {
            if(canBeKicked == true)
            {
                isMoving = true;
                canHitEnemy = true;
                StartCoroutine(KickCoroutine());
            }
        }

        public void DealDamage(GameObject obj) {
            Enemy enemy = obj.GetComponent<Enemy>();
            if (canHitEnemy == true) {
                damager.DealDamage(enemy.damageable);
                pusher.PushDealDamage(enemy.rb);
            }
        }

        public IEnumerator KickCoroutine() {
            movable.moveSpeed = kickAcceleration;
            movable.maxHoriSpeed = kickSpeed;
            movable.Push(kickJump);
            while (isMoving == true && kickTimer < kickDuration) {
                movable.MoveRight();
                kickTimer += Time.deltaTime;
                yield return wait;
            }

            gameObject.SetActive(false);
        }
        
        public IEnumerator DeathCoroutine()
        {
            canBeKicked = false;
            movable.SetSpeedLimited(false);
            movable.Push(deathForce);
            yield return waitDeath;

            canBeKicked = true;
            movable.SetSpeedLimited(true);
        }

        public void Setup() {
            canHitEnemy = false;
            movable.ResetValues();
            kickTimer = 0.0f;
        }

        public void Setdown() {
            movable.ResetValues();
            canHitEnemy = false;
        }

        private void OnEnable() {
            damageable.OnDeath += Death;
            enemyTrigger.OnCollide += DealDamage;
            kickTrigger.OnCollide += Kick;
        }

        private void OnDisable() {
            damageable.OnDeath -= Death;
            enemyTrigger.OnCollide -= DealDamage;
            kickTrigger.OnCollide -= Kick;
        }

        private void OnDestroy()
        {
            obstComp.OnSetup -= Setup;
        }
    }

}
