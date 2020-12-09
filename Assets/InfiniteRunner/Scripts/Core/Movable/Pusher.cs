using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    public class Pusher : MonoBehaviour
    {

        [Header("GameObject Reference")]
        public Rigidbody2D rb;

        public Vector2 forceDealDamage;
        public Vector2 forceTakeDamage;

        public void PushDealDamage(Rigidbody2D rb)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(forceDealDamage, ForceMode2D.Impulse);
        }

        public void PushTakeDamage(Rigidbody2D rb)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(forceTakeDamage, ForceMode2D.Impulse);
        }

        public void Push(Rigidbody2D rb, Vector2 force) {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

}
