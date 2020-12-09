using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Core;

namespace InfiniteRunner {
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(ManualMove))]
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerTravelScore))]
    public class Player : MonoBehaviour {
        public delegate void PlayerDelegate();
        public static event PlayerDelegate OnDeath;
        public static event PlayerDelegate OnOutOfBounds;

        [Header("Scriptable Reference")]
        public SFXSet sfxDamage;
        public SFXSet sfxDeath;

        [Header("GameObject Reference")]
        public Damageable damageable;
        public Movable movable;
        public ManualMove movement;
        public PlayerHealth pHealth;
        public PlayerTravelScore pScore;
        public Rigidbody2D rb;
        public SpriteRenderer sr;
        public Animator anim;

        [Header("Configuration")]
        public float damageWait = 1.5f;
        public Color hurtColor;
        public Color defaultColor;

        private bool canBeDamaged = true;

        private WaitForSeconds damageTimer;

        private void Awake() {
            if (damageable == null) {
                damageable = GetComponent<Damageable>();
            }

            if (movable == null)
            {
                movable = GetComponent<Movable>();
            }

            if (movement == null) {
                movement = GetComponent<ManualMove>();
            }

            if (pHealth == null) {
                pHealth = GetComponent<PlayerHealth>();
            }

            if(pScore == null)
            {
                pScore = GetComponent<PlayerTravelScore>();
            }

            if (rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }

            if (sr == null)
            {
                sr = GetComponentInChildren<SpriteRenderer>();
                Debug.Log("Forgot to reference sr", this);
            }

            damageTimer = new WaitForSeconds(damageWait);

            hurtColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
            defaultColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }

        public void Death() {
            anim.SetBool("IsDead", true);
            movement.SetMovement(false);
            pScore.SetActive(false);
            sfxDeath.Play();
            OnDeath?.Invoke();
        }

        public void TakeDamage()
        {
            if(damageable.health > 0)
            {
                StartCoroutine(DamageCoroutine());
            }
        }

        public IEnumerator DamageCoroutine()
        {
            sfxDamage.Play();
            canBeDamaged = false;
            gameObject.layer = (int)Meta_Layers.layers.PlayerDamaged;
            sr.color = hurtColor;
            yield return damageTimer;
            canBeDamaged = true;
            gameObject.layer = (int)Meta_Layers.layers.Player;
            sr.color = defaultColor;
        }

        public bool CanBeDamaged()
        {
            return canBeDamaged;
        }

        public void OutOfBounds()
        {
            OnOutOfBounds?.Invoke();
        }

        public void Setup() {
            anim.SetBool("IsDead", false);
            movement.SetMovement(true);
            pScore.SetActive(true);
            pHealth.ResetHealth();
        }

        public void Setdown() {

        }

        private void OnEnable() {
            GameStartManager.OnSetup += Setup;
            GameEndManager.OnGameEnd += Setdown;
            damageable.OnDamage += TakeDamage;
            damageable.OnDeath += Death;
            movement.OnOutOfBounds += OutOfBounds;
        }

        private void OnDisable() {
            GameStartManager.OnSetup -= Setup;
            GameEndManager.OnGameEnd -= Setdown;
            damageable.OnDamage -= TakeDamage;
            damageable.OnDeath -= Death;
            movement.OnOutOfBounds -= OutOfBounds;
        }
    }

}
