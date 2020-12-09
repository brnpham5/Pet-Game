using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner {
    public class LoseArea : MonoBehaviour {
        public delegate void LoseDelegate();
        public static event LoseDelegate OnLose;

        [Header("Scene Reference")]
        public Player player;

        [Header("Scriptable Reference")]
        public PlatformConfigData data;

        private float posY;
        private float posZ;

        private void Awake() {
            posY = data.DeathLevel;
            posZ = this.transform.position.z;

        }

        private void Start() {
            this.transform.position = new Vector3(0, data.DeathLevel, 0);
        }

        private void FixedUpdate() {
            this.transform.position = new Vector3(player.transform.position.x, posY, posZ);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            GameObject other = collision.gameObject;
            if (other.CompareTag("Player")){
                OnLose?.Invoke();
            }
        }
    }

}
