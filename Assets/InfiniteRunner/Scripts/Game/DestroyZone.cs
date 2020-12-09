using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    /// <summary>
    /// Moves the zone based on camera position
    /// </summary>
    public class DestroyZone : MonoBehaviour {
        [Header("Scriptable Reference")]
        public PlatformConfigData platformData;

        [Header("Scene Reference")]
        public Camera mainCamera;

        private float posY;
        private float posZ;

        private void Awake() {
            this.posY = this.transform.position.y;
            this.posZ = this.transform.position.z;

            if (mainCamera == null) {
                mainCamera = Camera.main;
            }
        }

        private void FixedUpdate() {
            this.transform.position = new Vector3(mainCamera.transform.position.x + platformData.DestroyOffset - platformData.CameraOffset, this.posY, this.posZ);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;

            if (obj.CompareTag(Meta_Tags.floor)){
                Platform platform = obj.GetComponent<Platform>();
                platform.Setdown();
            } else if (obj.CompareTag(Meta_Tags.obstacle))
            {
                ObstacleTilemap obst = obj.GetComponent<ObstacleTilemap>();
                obst.Setdown();
            }

        }
    }

}
