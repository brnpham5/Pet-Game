using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    public class ComponentCollection : MonoBehaviour
    {
        public List<ObstacleComponent> components;

        public void Setup()
        {
            components.ForEach(component =>
            {
                component.Setup();
            });
        }

        public void Setdown()
        {
            components.ForEach(component =>
            {
                component.Setdown();
            });
        }
    }

}
