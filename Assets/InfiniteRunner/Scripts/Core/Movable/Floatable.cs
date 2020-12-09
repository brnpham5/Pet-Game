using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Floatable : MonoBehaviour {

        public Rigidbody2D rb;

        public float moveSpeed = 1.0f;
        public Vector2 velocity;

        private void Awake() {
            if(rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }

            rb.isKinematic = true;
        }

        public void MoveUp()
        {
            rb.MovePosition(rb.position + new Vector2(0, moveSpeed) * Time.fixedDeltaTime);
        }

        public void MoveRight()
        {
            rb.MovePosition(rb.position + new Vector2(moveSpeed, 0) * Time.fixedDeltaTime);
        }
        
        public void MoveDown()
        {
            rb.MovePosition(rb.position + new Vector2(0, -moveSpeed) * Time.fixedDeltaTime);
        }

        public void MoveLeft()
        {
            rb.MovePosition(rb.position + new Vector2(-moveSpeed, 0) * Time.fixedDeltaTime);
        }

        public void Move()
        {
            rb.MovePosition(rb.position + this.velocity * moveSpeed * Time.fixedDeltaTime);
        }

        public void MoveTowards(Vector2 pos)
        {
            Vector2 nextPos = Vector2.MoveTowards(rb.position, pos, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(nextPos);
        }
    }
}
