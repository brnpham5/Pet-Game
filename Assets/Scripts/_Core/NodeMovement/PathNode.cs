using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core {
    using UnityEngine;

    public class PathNode : MonoBehaviour {
        public PathNodeEvent mOnArriveEvent;
        public PathNodeEvent mOnLeaveEvent;

        public enum eMovementType {
            Linear,
            Cos,
            Quadratic
        }

        public eMovementType mMovementType = eMovementType.Linear;

        // delay in this point in seconds
        public float mDelay = 0.0f;


        public float mSpeedModifier = 1.0f;

        // -------------------------------------------------------------------------
        public float Delay {
            get {
                return mDelay;
            }
        }

        // -------------------------------------------------------------------------
        public eMovementType MovementType {
            get {
                return mMovementType;
            }
        }

        // -------------------------------------------------------------------------
        public virtual void OnLeaveEvent(PathNodeEvent aEvent) {
            if (aEvent != null)
                aEvent.ProcessEvent(gameObject);
        }

        // -------------------------------------------------------------------------
        public virtual void OnArriveEvent(PathNodeEvent aEvent) {
            if (aEvent != null)
                aEvent.ProcessEvent(gameObject);
        }

        // -------------------------------------------------------------------------
        public float SpeedModifier {
            get {
                return mSpeedModifier;
            }
        }
    }
}