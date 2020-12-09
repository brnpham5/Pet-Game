using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    public class Rewardable : MonoBehaviour
    {
        [Header("Scriptable Reference")]
        public ScriptableInventory inventory;

        [Header("Configuration")]
        public List<PickupData> rewards;

        public void Grant()
        {
            rewards.ForEach(item =>
            {
                inventory.ApplyChange(item.type, item.value);
            });
        }
    }

}
