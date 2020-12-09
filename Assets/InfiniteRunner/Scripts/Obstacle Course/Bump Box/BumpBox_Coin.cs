using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner {
    [RequireComponent(typeof(Rewardable))]    
    public class BumpBox_Coin : BumpBox {
        public Rewardable rewardable;

        protected override void Awake() {
            base.Awake();
            if (rewardable == null) {
                rewardable = GetComponent<Rewardable>();
            }
        }

        protected override void Activate() {
            rewardable.Grant();
        }
    }
}
