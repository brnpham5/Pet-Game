using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    public class ComponentCollection : MonoBehaviour
    {
        public List<ObstacleComponent> components = new List<ObstacleComponent>();

        private void Awake()
        {
            ObstacleComponent[] children = GetComponentsInChildren<ObstacleComponent>();
            for (int i = 0; i < children.Length; i++)
            {
                components.Add(children[i]);
            }
        }

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
