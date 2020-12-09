using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Floatable))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BehaviourFloat : EnemyBehaviour
    {
        public Floatable floatable;
        public Rigidbody2D rb;

        private void Awake()
        {
            if(damageable == null)
            {
                damageable = GetComponent<Damageable>();
            }

            if(floatable == null)
            {
                floatable = GetComponent<Floatable>();
            }

            if(rb == null)
            {
                rb = GetComponent<Rigidbody2D>();
            }
        }

        public override void Move()
        {
            floatable.Move();
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

        public override void Setup()
        {
            rb.isKinematic = true;
        }

        public override void Death()
        {
            base.Death();
            rb.isKinematic = false;
            isActive = false;
        }
    }

}
