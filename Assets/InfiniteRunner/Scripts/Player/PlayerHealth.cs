using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace InfiniteRunner {
    [RequireComponent(typeof(Damageable))]
    public class PlayerHealth : MonoBehaviour {
        [Header("Scriptable Reference")]
        public ScriptableInventory inventory;
        public SFXSet sfxDamage;

        [Header("GameObject Reference")]
        public Damageable damageable;

        private void Awake() {
            ResetHealth();
        }

        /// <summary>
        /// Change health in inventory to value of damageable.
        /// </summary>
        public void ChangeHealth() {
            this.inventory.health.SetValue(damageable.GetHealth());
        }

        /// <summary>
        /// Set health of damageable (which then sets value in inventory)
        /// </summary>
        /// <param name="health"></param>
        public void SetHealth(int health) {
            damageable.SetHealth(health);
        }

        /// <summary>
        /// Reset health according to inventory
        /// </summary>
        public void ResetHealth() {
            damageable.SetMaxHealth(this.inventory.health.data.max);
            damageable.SetHealth(this.inventory.health.data.startingAmt);
        }

        private void OnEnable() {
            damageable.OnChange += ChangeHealth;
        }

        private void OnDisable() {
            damageable.OnChange -= ChangeHealth;
        }
    }

}
