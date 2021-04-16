using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace InfiniteRunner {
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class ManualMove : MonoBehaviour {
        public delegate void MoveDelegate();
        public event MoveDelegate OnOutOfBounds;

        [Header("Scriptable reference")]
        public SFXSet sfxJump;

        [Header("Gameobject Reference")]
        public Movable movable;
        public MaskTouch groundTouch;
        public ParticleSystem psLanding;
        public Rigidbody2D rb;

        [Header("Configuration")]
        public float xBound = 100f;
        public float jumpQueueTime = 0.15f;
        public float dropJumpTime = 0.15f;
        public int jumpMax = 2;

        public bool autoMove = true;
        //Whether or not this object can move
        private bool canMove = true;
        
        //Jump queue for when player wants to jump before landing
        private bool jumpQueue = false;
        private float jumpQueueTimer = 0f;

        //Jump count for number of jumps before landing
        private int jumpCount;

        //Drop Jump queue for when player becomes ungrounded but still wants to jump
        public bool dropJumpQueue = false;
        public float dropJumpTimer = 0f;

        //Whether or not to slow velocity after releasing jump input
        private bool jumpDampen;

        private WaitForFixedUpdate waitFixed = new WaitForFixedUpdate();

        private void Update() {
            GetInput();
        }

        public void SetMovement(bool status) {
            canMove = status;
        }

        public void SetAutoMovement(bool status)
        {
            autoMove = status;
        }

        private void FixedUpdate() {
            if (autoMove == true && canMove == true) {
                movable.MoveRight();
            }

            if(gameObject.transform.position.x >= xBound)
            {
                OnOutOfBounds?.Invoke();
            }
        }

        public IEnumerator DropJumpCoroutine()
        {
            //Set drop jump queue to allow jumping for a time after falling
            dropJumpQueue = true;
            while (dropJumpTimer > 0)
            {
                
                dropJumpTimer -= Time.deltaTime;
                yield return waitFixed;
            }

            dropJumpQueue = false;
            //If movable gets ungrounded without jumping(take away one jump)
            if (jumpCount == 0)
            {
                jumpCount++;
            }
        }

        public void Grounded()
        {
            if(rb.velocity.y > 0)
            {

            } else
            {
                jumpCount = 0;
                psLanding.Play();
            }
        }

        public void Ungrounded()
        {
            dropJumpTimer = dropJumpTime;
            StartCoroutine(DropJumpCoroutine());
        }

        public IEnumerator JumpQueueCoroutine()
        {
            jumpQueue = true;
            while(jumpQueueTimer > 0 && jumpQueue == true)
            {
                if(movable.isGrounded == true)
                {
                    Jump();
                    jumpQueue = false;
                }
                jumpQueueTimer -= Time.deltaTime;
                yield return waitFixed;
            }
            jumpQueue = false;
        }

        private void GetInput() {

            if(canMove == true) {
                if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    if(movable.isGrounded || dropJumpQueue == true || jumpCount < jumpMax)
                    {
                        Jump();
                    } else
                    {
                        jumpQueueTimer = jumpQueueTime;
                        if(jumpQueue == false)
                        {
                            StartCoroutine(JumpQueueCoroutine());
                        }
                    }
                }
                /* Jump Dampen Feature
                else if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    //Dampen velocity if character is moving upwards upon releasing input
                    if(jumpDampen == true && rb.velocity.y > 0)
                    {
                        movable.rb.velocity = new Vector2(movable.rb.velocity.x, movable.rb.velocity.y / 2);
                        jumpDampen = false;
                    }
                }
                */
                else if (Input.GetKey(KeyCode.RightArrow)) {
                    movable.MoveRight();
                }
                else if (Input.GetKey(KeyCode.LeftArrow)) {
                    movable.MoveLeft();
                }
            }
        }

        public void Jump()
        {
            sfxJump.Play();
            movable.Jump();
            jumpCount++;
            jumpDampen = true;
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
