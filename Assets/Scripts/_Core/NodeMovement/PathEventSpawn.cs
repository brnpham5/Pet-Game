using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core {
    public class PathEventSpawn : PathNodeEvent {
        public Transform mSpawnPoint;
        public GameObject mSpawnObject;

        // -------------------------------------------------------------------------
        public override void ProcessEvent(GameObject aGameObject) {
            // check for undefined spawn object or point
            if (mSpawnObject == null) {
                Debug.LogError("Spawn object is not defined");
                return;
            }
            else if (mSpawnPoint == null) {
                Debug.LogError("Spawn point is not defined");
                return;
            }

            Instantiate(mSpawnObject,
            mSpawnPoint.position, mSpawnPoint.rotation);
        }
    }
}
