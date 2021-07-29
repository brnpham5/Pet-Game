using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Floatable))]
    public class BehaviourSpider : EnemyBehaviour
    {
        public delegate void SpiderDelegate();
        public event SpiderDelegate OnEatFly;

        public Floatable floatable;

        private void Awake()
        {
            if (damageable == null)
            {
                damageable = GetComponent<Damageable>();
            }

            if (floatable == null)
            {
                floatable = GetComponent<Floatable>();
            }
        }

        public override void Move()
        {
            floatable.MoveDown();
        }

        public override IEnumerator BehaviourCoroutine()
        {
            while (isActive)
            {
                if (isMoving)
                {
                    Move();
                }
                yield return wait;
            }
        }

        public void FloatStop()
        {
            StopBehaviour();
        }

        public override void Setup()
        {
            
        }

        public override void Death()
        {
            base.Death();
            isActive = false;
            StopBehaviour();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;
            if (obj.CompareTag(Meta_Tags.floatStop))
            {
                OnEatFly?.Invoke();
            }
        }
    }

}
