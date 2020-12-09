using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace InfiniteRunner {
    [RequireComponent(typeof(ObstacleComponent))]
    public abstract class BumpBox : MonoBehaviour {
        [Header("Scriptable Reference")]
        public SFXSet sfxBox;

        [Header("GameObject Reference")]
        public Animator anim;
        public ObstacleComponent obstComp;
        public TagTrigger bumpTrigger;

        protected bool isActive;

        protected virtual void Awake() {
            

            if (obstComp == null) {
                obstComp = GetComponent<ObstacleComponent>();
            }

            if(bumpTrigger == null) {
                bumpTrigger = GetComponent<TagTrigger>();
            }

            obstComp.OnSetup += Setup;
            isActive = true;
            anim.SetBool("IsActive", true);
        }

        protected virtual void Bump(GameObject obj) {
            if (isActive == true) {
                isActive = false;
                sfxBox.Play();
                anim.SetBool("IsActive", false);
                Activate();
            }
        }

        protected abstract void Activate();

        public virtual void Setup() {
            isActive = true;
            anim.SetBool("IsActive", true);
        }

        protected virtual void AddListeners() {
            
            bumpTrigger.OnCollide += Bump;
        }

        protected virtual void RemoveListeners() {
            bumpTrigger.OnCollide -= Bump;
        }

        protected void OnEnable() {
            AddListeners();
        }

        protected void OnDisable() {
            RemoveListeners();
        }

        private void OnDestroy()
        {
            obstComp.OnSetup -= Setup;
        }
    }
}
