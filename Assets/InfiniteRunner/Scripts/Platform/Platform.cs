using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    public abstract class Platform : MonoBehaviour {
        [Header("GameObject Reference")]
        public SpriteRenderer sr;

        private float width;

        public float GetWidth()
        {
            return width;
        }

        public virtual void Setup(float width) {
            this.gameObject.SetActive(true);

            this.width = width;
            sr.size = new Vector2(width, 1);
        }

        public virtual void Setdown() {
            this.gameObject.SetActive(false);
        }
    }

}
