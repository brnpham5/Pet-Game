using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core {
    public abstract class PathNodeEvent : MonoBehaviour {
        public float mDelayBefore = 0.0f;
        public float mDelayAfter = 0.0f;
        public PathNodeEvent mNextEvent = null;

        // -------------------------------------------------------------------------
        public virtual void ProcessEvent(GameObject aGameObject) {
            StartCoroutine(Process(aGameObject));
        }

        // -------------------------------------------------------------------------
        protected virtual void DoProcess(GameObject aGameObject) {
            Debug.Log(gameObject.name + ": processing event");
        }

        // -------------------------------------------------------------------------
        protected IEnumerator Process(GameObject aGameObject) {
            if (mDelayBefore > 0)
                yield return new WaitForSeconds(mDelayBefore);

            DoProcess(aGameObject);

            if (mDelayAfter > 0)
                yield return new WaitForSeconds(mDelayAfter);

            if (mNextEvent != null)
                mNextEvent.ProcessEvent(aGameObject);
        }
    }

}
