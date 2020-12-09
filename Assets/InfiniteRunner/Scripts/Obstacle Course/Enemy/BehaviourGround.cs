using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [RequireComponent(typeof(Movable))]
    public class BehaviourGround : EnemyBehaviour {
        public Movable movable;

        public override void Move() {
            movable.MoveLeft();
        }

        public override IEnumerator BehaviourCoroutine() {
            while (isActive) {
                if (isMoving) {
                    Move();
                }
                yield return wait;
            }
            yield return wait;
        }

        public override void Setup()
        {
            
        }
    }
}

