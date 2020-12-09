using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{

    public class Stretchable : MonoBehaviour
    {
        [Header("GameObject Reference")]
        public SpriteRenderer sr;

        [Header("Scene Reference")]
        public Transform target;

        private bool isActive = true;

        public void Stretch()
        {
            float distance = Vector2.Distance(this.transform.localPosition, target.localPosition);
            sr.size = new Vector2(1, distance);
        }

        public void Update()
        {
            if(isActive == true)
                Stretch();
        }

        public void SetActive(bool value)
        {
            isActive = value;
        }
    }

}
