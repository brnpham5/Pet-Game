using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    public class Unlockable : MonoBehaviour
    {
        public delegate void UnlockDelegate();
        public event UnlockDelegate OnUnlock;

        [Header("Scriptable Reference")]
        public PickupData key;
        public ScriptableInventory inventory;
        
        private void Awake()
        {
            if (!gameObject.tag.Equals(Meta_Tags.unlockable))
            {
                gameObject.tag = Meta_Tags.unlockable;
                Debug.Log("Forgot to tag", this);
            }
        }

        public void Unlock()
        {
            if (inventory.CanAfford(key.type, key.value))
            {
                inventory.ApplyChange(key.type, key.value);
                OnUnlock?.Invoke();
            }
        }
    }

}
