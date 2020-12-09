using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    public class TagTrigger : MonoBehaviour {
        public delegate void ColliderDelegate(GameObject obj);
        public event ColliderDelegate OnCollide;

        public Meta_Tags.tags tags;

        private void OnTriggerEnter2D(Collider2D collision) {
            GameObject obj = collision.gameObject;

            if (obj.CompareTag(tags.ToString())) {
                OnCollide?.Invoke(obj);
            }
        }
    }

}
