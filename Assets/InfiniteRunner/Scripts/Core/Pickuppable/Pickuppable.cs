using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    
    public class Pickuppable : MonoBehaviour
    {
        [Header("Scriptable Reference")]
        public PickupData data;
        public ScriptableInventory inventory;

        private void Awake()
        {
            if (!gameObject.tag.Equals(Meta_Tags.pickup))
            {
                gameObject.tag = Meta_Tags.pickup;
                Debug.Log("Forgot to tag", this);
            }
        }

        public void Activate()
        {
            PickupItem(this.data);
            
        }

        /// <summary>
        /// Pickup item by data
        /// </summary>
        /// <param name="data"></param>
        public void PickupItem(PickupData data)
        {
            switch (data.type)
            {
                case ItemType.score:
                    PickupScore(data.value);
                    break;
                case ItemType.energy:
                    PickupEnergy(data.value);
                    break;
                case ItemType.health:
                    PickupHealth(data.value);
                    break;
                case ItemType.key_lrg:
                    PickupKeyLrg(data.value);
                    break;
                case ItemType.key_sml:
                    PickupKeySml(data.value);
                    break;
            }
        }

        public void PickupScore(int value)
        {
            inventory.score.ApplyChange(value);
        }

        public void PickupEnergy(int value)
        {
            inventory.energy.ApplyChange(value);
        }

        public void PickupHealth(int value)
        {
            inventory.health.ApplyChange(value);
        }

        public void PickupKeyLrg(int value)
        {
            inventory.keyLrg.ApplyChange(value);
        }

        public void PickupKeySml(int value)
        {
            inventory.keySml.ApplyChange(value);
        }
    }

}
