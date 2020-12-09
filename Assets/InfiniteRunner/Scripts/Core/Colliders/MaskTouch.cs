using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    public class MaskTouch : MonoBehaviour {
        public delegate void ColliderDelegate();
        public event ColliderDelegate OnTouch;
        public event ColliderDelegate OnUntouch;

        public Collider2D cldr;
        public LayerMask mask;

        public bool isTouching;
        public bool touching;

        private void FixedUpdate()
        {
            //Leaving as reference to an alternate method to detect the ground (in case GroundCollider is hard to use)
            touching = Physics2D.IsTouchingLayers(cldr, mask);

            if (touching == true)
            {
                Grounded();
            }
            else if (touching == false)
            {
                Ungrounded();
            }
        }

        public void Grounded()
        {
            if(isTouching == false)
            {
                isTouching = true;
                OnTouch?.Invoke();
            }
        }

        public void Ungrounded()
        {
            if(isTouching == true)
            {
                isTouching = false;
                OnUntouch?.Invoke();
            }
        }
    }

}
