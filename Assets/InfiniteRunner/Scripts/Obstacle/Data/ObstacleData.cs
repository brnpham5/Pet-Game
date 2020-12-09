using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    [System.Serializable]
    public class ObstacleData {
        [Header("Configuration")]
        public float weight = 10;
        [Range(0, 1)]
        public float weightMod = 1;

        [Header("Asset Reference")]
        public ObstacleTilemap obstacle;

        public ObstacleTilemap GetObstacle()
        {
            return obstacle;
        }
    }
}
