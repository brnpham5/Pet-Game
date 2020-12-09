using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    /// <summary>
    /// Makes the camera follow the player
    /// </summary>
    public class CameraController : MonoBehaviour {
        [Header("Scene Reference")]
        public GameObject player;

        [Header("Scriptable Reference")]
        public PlatformConfigData data;

        private float posZ;
        private float posY;
        
        private void Start() {
            posY = this.transform.position.y;
            posZ = this.transform.position.z;
        }

        // Update is called once per frame
        void FixedUpdate() {
            float posX = Mathf.Lerp(this.transform.position.x, player.transform.position.x + data.CameraOffset, data.CameraLerpValue);
            //float posY = Mathf.Lerp(this.transform.position.y, player.transform.position.y, lerpValue);

            this.transform.position = new Vector3(posX, this.posY, this.posZ);
        }
    }

}
