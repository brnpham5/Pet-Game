using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [RequireComponent(typeof(Damageable))]
    public abstract class EnemyBehaviour : MonoBehaviour {

        public bool isActive = true;
        public bool isMoving;

        [Header("GameObject Reference")]
        public Damageable damageable;
        public TagTrigger aggroTrigger;

        public WaitForFixedUpdate wait = new WaitForFixedUpdate();

        private EnemyBehaviour instance;
        private void Awake() {
            if(instance != null) {
                Destroy(this.gameObject);
                Debug.Log("Only one behaviour allowed");
            } else {
                instance = this;
            }

            if(damageable == null)
            {
                damageable = GetComponent<Damageable>();
            }
        }

        public void SetActive(bool value) {
            isActive = value;
        }

        public void SetMoving(bool value) {
            isMoving = value;
        }

        /// <summary>
        /// Start behaviour of enemy
        /// </summary>
        public virtual void StartBehaviour() {
            SetMoving(true);
            StartCoroutine(BehaviourCoroutine());
        }

        public virtual void StopBehaviour() {
            SetMoving(false);
            StopAllCoroutines();
        }

        public abstract void Move();

        public abstract void Setup();

        public virtual void Death()
        {
            StopBehaviour();
        }

        public virtual void AggroTrigger(GameObject obj)
        {
            if (isActive == true)
            {
                StartBehaviour();
            }
        }

        public abstract IEnumerator BehaviourCoroutine();

        protected virtual void AddListeners()
        {
            aggroTrigger.OnCollide += AggroTrigger;
            damageable.OnDeath += Death;
        }

        protected virtual void RemoveListeners()
        {
            aggroTrigger.OnCollide -= AggroTrigger;
            damageable.OnDeath -= Death;
        }

        protected virtual void OnEnable()
        {
            AddListeners();
        }

        protected virtual void OnDisable()
        {
            RemoveListeners();
        }
    }

}
