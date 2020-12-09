using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    public class Movable : MonoBehaviour {

        [Header("Editor Reference")]
        public Animator anim;
        public SpriteRenderer sr;
        public Rigidbody2D rb;
        public Collider2D cldr;
        public MaskTouch groundTouch;

        [Header("Configuration")]
        public float moveSpeed = 10.0f;
        public float maxHoriSpeed = 10.0f;
        public float jumpForce = 200.0f;

        private float initMoveSpeed;
        private float initMaxHoriSpeed;
        private float initJumpForce;

        public bool isGrounded = false;
        public bool isMoving = false;
        public bool isSpeedLimited = true;

        private void Awake() {
            initMoveSpeed = moveSpeed;
            initMaxHoriSpeed = maxHoriSpeed;
            initJumpForce = jumpForce;
        }

        private void FixedUpdate() {
            Vector2 hori;

            if (rb.velocity.x >= 0.1) {
                isMoving = true;
                anim.SetBool("IsMoving", true);
                sr.flipX = false;
            }
            else if (rb.velocity.x <= -0.1) {
                isMoving = true;
                anim.SetBool("IsMoving", true);
                sr.flipX = true;
            }
            else {
                isMoving = false;
                anim.SetBool("IsMoving", false);
            }

            if(isSpeedLimited == true)
            {
                //Prevent from moving past maxSpeed;
                if (Mathf.Abs(rb.velocity.x) > maxHoriSpeed)
                {
                    // and finally asign the new vel
                    hori = rb.velocity.normalized * maxHoriSpeed;
                    rb.velocity = new Vector2(hori.x, rb.velocity.y);
                }
            }
        }

        public void SetSpeedLimited(bool value)
        {
            this.isSpeedLimited = value;
        }

        public void Grounded() {
            isGrounded = true;
            anim.SetBool("IsGrounded", true);
        }

        public void Ungrounded() {
            isGrounded = false;
            anim.SetBool("IsGrounded", false);
        }

        public void Jump() {
            Vector2 force = new Vector2(0, jumpForce);
            Push(force);
        }

        public void Push(Vector2 force) {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        public void MoveRight() {
            Vector2 force = new Vector2(moveSpeed, 0);

            rb.AddForce(force);
        }

        public void MoveLeft() {
            Vector2 force = new Vector2(-moveSpeed, 0);

            rb.AddForce(force);
        }

        public void ResetValues() {
            this.moveSpeed = this.initMoveSpeed;
            this.maxHoriSpeed = this.initMaxHoriSpeed;
            this.jumpForce = this.initJumpForce;
        }

        private void OnEnable()
        {
            groundTouch.OnTouch += Grounded;
            groundTouch.OnUntouch += Ungrounded;
        }

        private void OnDisable()
        {
            groundTouch.OnTouch -= Grounded;
            groundTouch.OnUntouch -= Ungrounded;
        }
    }
}
