using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    public class Damager : MonoBehaviour
    {
        public int value;

        public void DealDamage(Damageable damageable)
        {
            damageable.ApplyChange(-value);
        }

        public void DealDamage(Damageable damageable, int value)
        {
            damageable.ApplyChange(-value);
        }
    }

}
