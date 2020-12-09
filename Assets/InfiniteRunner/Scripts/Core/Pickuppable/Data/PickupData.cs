using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [CreateAssetMenu(fileName = "Pickup Data", menuName = "Runner/Config/Pickup Data")]
    public class PickupData : ScriptableObject {
        public ItemType type;
        public int value;
    }

}
