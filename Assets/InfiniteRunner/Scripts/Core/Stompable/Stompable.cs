using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace InfiniteRunner {
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Pusher))]
    public class Stompable : MonoBehaviour {
        [Header("Scriptable Reference")]
        public SFXSet sfxStomp;

        [Header("GameObject Reference")]
        public Damageable damageable;
        public Pusher pusher;

        private bool isActive;

        public TagTrigger stompTrigger;

        private void Awake() {
            if(damageable == null) {
                damageable = GetComponent<Damageable>();
            }

            if(pusher == null) {
                pusher = GetComponent<Pusher>();
            }
        }

        /// <summary>
        /// Sets this on (true) or off (false)
        /// </summary>
        /// <param name="value"></param>
        public void SetActive(bool value) {
            isActive = value;
        }

        public void Stomp(GameObject obj) {
            if (isActive == true) {
                Player player = obj.GetComponent<Player>();
                damageable.SetHealth(0);
                pusher.PushDealDamage(player.rb);
                sfxStomp.Play();
            }
        }

        public void Setup() {
            isActive = true;
        }

        private void OnEnable() {
            stompTrigger.OnCollide += Stomp;
            Setup();
        }

        private void OnDisable() {
            stompTrigger.OnCollide -= Stomp;
        }
    }

}
