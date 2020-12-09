using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    public class Triggerable : MonoBehaviour {
        public delegate void TriggerDelegate();
        public event TriggerDelegate OnTrigger;

        public void Activate() {
            OnTrigger?.Invoke();
        }
    }
}
