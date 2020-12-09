using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace InfiniteRunner {
    [CreateAssetMenu(fileName = "Inventory", menuName = "Runner/Runtime/Inventory")]
    public class ScriptableInventory : ScriptableObject {
        public InventoryData energy;
        public InventoryData health;
        public InventoryData keyLrg;
        public InventoryData keySml;
        public InventoryData score;

        public int GetValue(ItemType type)
        {
            switch (type)
            {
                case ItemType.score:
                    return score.GetValue();
                case ItemType.energy:
                    return energy.GetValue();
                case ItemType.health:
                    return health.GetValue();
                case ItemType.key_lrg:
                    return keyLrg.GetValue();
                case ItemType.key_sml:
                    return keySml.GetValue();
                default:
                    return 0;
            }
        }

        public void ApplyChange(ItemType type, int value)
        {
            switch (type)
            {
                case ItemType.score:
                    score.ApplyChange(value);
                    break;
                case ItemType.energy:
                    energy.ApplyChange(value);
                    break;
                case ItemType.health:
                    health.ApplyChange(value);
                    break;
                case ItemType.key_lrg:
                    keyLrg.ApplyChange(value);
                    break;
                case ItemType.key_sml:
                    keySml.ApplyChange(value);
                    break;
            }
        }

        public bool CanAfford(ItemType type, int value)
        {
            switch (type)
            {
                case ItemType.score:
                    return score.GetValue() >= value;
                case ItemType.energy:
                    return energy.GetValue() >= value;
                case ItemType.health:
                    return health.GetValue() >= value;
                case ItemType.key_lrg:
                    return keyLrg.GetValue() >= value;
                case ItemType.key_sml:
                    return keySml.GetValue() >= value;
                default:
                    return false;
            }
        }
    }
}
