using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {

    public class ObstacleSpawner : MonoBehaviour {
        [Header("Scriptable Reference")]
        public PlatformConfigData platformData;
        public CurrentLevelData currentLevel;

        [Header("Scene Reference")]
        public GameObject obstaclePool;

        [Header("Runtime")]
        public Dictionary<ObstacleData, ObstaclePool > pool = new Dictionary<ObstacleData, ObstaclePool>();
        private ObstacleData nextObst;

        public float totalWeight;
        public int qIndex = 0;
        public List<ObstacleData> weightQueue = new List<ObstacleData>();

        private void InitializePool(ObstacleData data, float weight, float weightMod)
        {
            pool.Add(data, new ObstaclePool { 
                weight = weight,
                weightMod = weightMod,
                currentWeight = weight,
                pool = new List<ObstacleTilemap>()
            });
        }

        /// <summary>
        /// Add all obstacles in level to pool
        /// Initialize lists for pool
        /// </summary>
        public void SetupLevel() {
            if(pool.ContainsKey(currentLevel.data.resetObstacle) == false)
            {
                InitializePool(currentLevel.data.resetObstacle, 0, 0);
            }
            currentLevel.data.obstacles.ForEach(obData => {
                if(pool.ContainsKey(obData) == false) {
                    InitializePool(obData, obData.weight, obData.weightMod);
                }
            });
        }

        /// <summary>
        /// Set up initial values
        /// </summary>
        public void Setup()
        {
            SetupLevel();

            CalculateTotalWeight();

            this.nextObst = Roll();
            weightQueue.Add(this.nextObst);
        }

        private void CalculateTotalWeight()
        {
            totalWeight = 0;
            currentLevel.data.obstacles.ForEach(obData => {
                totalWeight += pool[obData].currentWeight;
            });
        }

        /// <summary>
        /// Clean up all current obstacles
        /// </summary>
        public void Setdown() {
            foreach (KeyValuePair<ObstacleData, ObstaclePool> keyValue in pool)
            {
                keyValue.Value.ResetWeight();
                keyValue.Value.pool.ForEach(item =>
                {
                   item.Setdown();
                });
            }
            nextObst = null;

            weightQueue.Clear();
        }

        public void SetupResetObst()
        {
            this.nextObst = currentLevel.data.resetObstacle;
        }

        /// <summary>
        /// Spawn a specific obstacle
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public ObstacleTilemap Spawn(ObstacleData obData)
        {
            ObstacleTilemap obstacle = GetPool(obData);

            return obstacle;
        }

        /// <summary>
        /// Spawn the next obstacle
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public ObstacleTilemap SpawnNext()
        {
            ObstacleTilemap obstacle = Spawn(this.nextObst);
            
            //Modify the weight so it does not occor too many times in a row.
            pool[this.nextObst].ApplyWeightMod();
            if(weightQueue.Count >= currentLevel.data.obstacles.Count)
            {
                pool[weightQueue[qIndex]].ResetWeight();
                weightQueue.RemoveAt(qIndex);
                qIndex++;
                if(qIndex >= currentLevel.data.obstacles.Count)
                {
                    qIndex = 0;
                }
            }

            CalculateTotalWeight();
            weightQueue.Add(this.nextObst);
            this.nextObst = Roll();

            return obstacle;
        }
        
        /// <summary>
        /// Position the obstacle according to it's data.
        /// </summary>
        /// <param name="obData"></param>
        /// <param name="obstacle"></param>
        public void PositionObstacle(ObstacleTilemap obstacle, float pos) {
            //Adjust position by obstacle center
            pos -= obstacle.GetCenter();
            obstacle.transform.position = new Vector3(pos, (float) (platformData.GroundLevel + 1.5), 0);
        }

        /// <summary>
        /// Roll for a random platform by weight
        /// </summary>
        /// <returns></returns>
        private ObstacleData Roll()
        {
            List<ObstacleData> obs = currentLevel.data.obstacles;
            float roll = Random.Range(0, totalWeight);
            ObstacleData data;

            float total = 0f;

            for (int i = 0; i < obs.Count; i++)
            {
                data = obs[i];
                total += pool[data].currentWeight;
                if (total >= roll)
                {
                    return obs[i];
                }
            }

            return obs[obs.Count - 1];
        }

        /// <summary>
        /// Get an object from the pool
        /// If nothing is available, instantiate a new one
        /// </summary>
        /// <param name="obData"></param>
        /// <returns></returns>
        public ObstacleTilemap GetPool(ObstacleData obData) {
            List<ObstacleTilemap> obsPool = pool[obData].pool;
            ObstacleTilemap obstacle;

            for (int i = 0; i < obsPool.Count; i++) {
               obstacle = obsPool[i];
                if (obstacle.isActiveAndEnabled == false) {
                    return obstacle;
                }
            }

            obstacle = Instantiate(obData.GetObstacle());
            obstacle.transform.parent = obstaclePool.transform;
            obsPool.Add(obstacle);
            return obstacle;
        }
    }

}
