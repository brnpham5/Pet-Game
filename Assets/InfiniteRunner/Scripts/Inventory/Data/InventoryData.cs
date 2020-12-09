using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace InfiniteRunner {
    [System.Serializable]
    public class InventoryData {
        public delegate void InventoryDelegate();
        public event InventoryDelegate OnChange;

        [Header("Runtime")]
        public IntVariable variable;

        [Header("Configuration")]
        public ItemData data;

        public int GetValue()
        {
            return variable.GetValue();
        }

        public void SetValue(int value)
        {
            if (value >= data.max)
            {
                this.variable.SetValue(data.max);
            }
            else if (value <= data.min)
            {
                this.variable.SetValue(data.min);
            }
            else
            {
                this.variable.SetValue(value);
            }

            OnChange?.Invoke();
        }

        public void ApplyChange(int value)
        {
            SetValue(this.variable.GetValue() + value);
        }

        public void SetVariable(IntVariable variable)
        {
            this.variable = variable;
            OnChange?.Invoke();
        }

        public void SetConfig(ItemData data)
        {
            this.data = data;


            if(this.variable.GetValue() > data.max)
            {
                SetValue(data.max);
            }
        }
    }
}
