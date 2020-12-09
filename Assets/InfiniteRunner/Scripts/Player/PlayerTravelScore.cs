using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    public class PlayerTravelScore : MonoBehaviour
    {
        [Header("Scriptable Reference")]
        public ScriptableInventory inventory;

        [Header("Configuration")]
        public float scoreInterval = 3.0f;
        public int scoreValue = 1;

        private float scoreTimer;
        private bool isRunning = true;

        // Update is called once per frame
        void Update()
        {
            if (isRunning == true)
            {
                scoreTimer += Time.deltaTime;
                if (scoreTimer >= scoreInterval)
                {
                    GrantReward();
                    scoreTimer = 0;
                }
            }
        }

        public bool IsRunning()
        {
            return isRunning;
        }

        public void SetActive(bool value)
        {
            isRunning = value;
        }

        private void GrantReward()
        {
            inventory.ApplyChange(ItemType.score, scoreValue);
        }
        
    }

}
