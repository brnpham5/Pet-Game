using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Runner/Config/Item Data")]
    public class ItemData: ScriptableObject
    {
        public int min;
        public int startingAmt;
        public int max;
        [Multiline]
        public string description;
    }

}
