using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [CreateAssetMenu(fileName = "Level Data", menuName = "Runner/Config/Level Data")]
    public class LevelData : ScriptableObject {
        public List<ObstacleData> obstacles;
        public ObstacleData resetObstacle;
    }
}
