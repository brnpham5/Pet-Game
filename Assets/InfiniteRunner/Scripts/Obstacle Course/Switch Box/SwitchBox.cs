using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [RequireComponent(typeof(ObstacleComponent))]
    [RequireComponent(typeof(Triggerable))]
    public class SwitchBox : MonoBehaviour {
        [Header("GameObject Reference")]
        public ObstacleComponent obstComp;
        public Triggerable triggerable;
        public Animator anim;
        
        public Collider2D cldrHori;
        public Collider2D cldrVert;

        private bool isActive;

        private void Awake() {
            if(obstComp == null) {
                obstComp = GetComponent<ObstacleComponent>();
            }

            if(triggerable == null) {
                triggerable = GetComponent<Triggerable>();
            }

            obstComp.OnSetup += Setup;

            Setup();

        }

        public void Activate() {
            if(isActive == false) {
                isActive = true;
                anim.SetBool("IsActive", true);
                ActivateColliders();
            }
        }

        public void ActivateColliders() {
            cldrHori.enabled = true;
            cldrVert.enabled = true;
        }

        public void DeactivateColliders() {
            cldrHori.enabled = false;
            cldrVert.enabled = false;
        }

        public void Setup() {
            isActive = false;
            anim.SetBool("IsActive", false);
            DeactivateColliders();
        }

        private void OnEnable() {
            triggerable.OnTrigger += Activate;
        }

        private void OnDisable() {
            triggerable.OnTrigger -= Activate;
        }

        private void OnDestroy()
        {
            obstComp.OnSetup -= Setup;

        }
    }

}
