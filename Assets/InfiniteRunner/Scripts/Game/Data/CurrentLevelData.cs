using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [CreateAssetMenu(fileName = "Current Level", menuName = "Runner/Runtime/Current Level")]
    public class CurrentLevelData : ScriptableObject {
        public LevelData data;
    }

}
