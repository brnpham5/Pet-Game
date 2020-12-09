using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    public class ObstaclePool
    {
        public float weight;
        public float weightMod;
        public float currentWeight;
        public List<ObstacleTilemap> pool;

        public void ApplyWeightMod()
        {
            this.currentWeight -= this.weight * weightMod;
            if (this.currentWeight <= 0)
            {
                this.currentWeight = 0;
            }
        }

        public void ResetWeight()
        {
            this.currentWeight = weight;
        }
    }

}
