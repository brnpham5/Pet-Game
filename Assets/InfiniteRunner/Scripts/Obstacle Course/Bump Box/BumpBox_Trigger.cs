using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner {
    public class BumpBox_Trigger : BumpBox {
        [Header("Scene Reference")]
        public Trigger trigger;

        protected override void Awake() {
            base.Awake();
            if(trigger == null) {
                Debug.Log("Forgot to reference trigger", this);
            }
        }

        protected override void Activate() {
            trigger.Activate();
        }
    }
}
