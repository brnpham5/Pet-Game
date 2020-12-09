using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner {
    public class Damageable : MonoBehaviour {
        public delegate void DamageableDelegate();
        public event DamageableDelegate OnChange;
        public event DamageableDelegate OnDamage;
        public event DamageableDelegate OnDeath;

        public int health;
        public int maxHealth;

        public int GetHealth() {
            return health;
        }

        public void SetMaxHealth(int value) {
            this.maxHealth = value;
        }

        /// <summary>
        /// Set health directly, cannot go over max or under min (0)
        /// </summary>
        /// <param name="health"></param>
        public void SetHealth(int health) {
            if(health > maxHealth) {
                this.health = maxHealth;
            } else if(health <= 0) {
                this.health = 0;
                OnDeath?.Invoke();
            } else {
                this.health = health;
            }
            OnChange?.Invoke();
        }

        /// <summary>
        /// Apply change to health by value
        /// </summary>
        /// <param name="value"></param>
        public void ApplyChange(int value) {
            int temp = this.health + value;
            SetHealth(temp);
            if(value < 0)
            {
                OnDamage?.Invoke();
            }
        }

        /// <summary>
        /// Reset health to max health;
        /// </summary>
        public void ResetHealth()
        {
            SetHealth(this.maxHealth);
        }
    }

}
