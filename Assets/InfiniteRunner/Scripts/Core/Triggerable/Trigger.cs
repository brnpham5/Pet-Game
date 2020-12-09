using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner {
    public class Trigger : MonoBehaviour {
        public List<Triggerable> triggerables;

        public void Activate() {
            triggerables.ForEach(trig => {
                trig.Activate();
            });
        }
    }
}
