using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace InfiniteRunner
{
    public class ObstacleTilemap : MonoBehaviour
    {
        [Header("GameObject Reference")]
        public TilemapCollider2D tmCldr;

        public List<ComponentCollection> components = new List<ComponentCollection>();

        public BoxCollider2D exitCollider;

        private float center;
        private float extent;
        private float width;

        private void Awake()
        {
            ComponentCollection[] children = GetComponentsInChildren<ComponentCollection>();
            for(int i = 0; i < children.Length; i++)
            {
                components.Add(children[i]);
            }

            center = tmCldr.bounds.center.x;
            extent = tmCldr.bounds.extents.x;
            width = extent * 2;


            exitCollider.offset = new Vector2(tmCldr.bounds.center.x, tmCldr.bounds.center.y);
            exitCollider.size = new Vector2(width, 10);
        }

        public float GetCenter()
        {
            return center;
        }

        public float GetExtent()
        {
            return extent;
        }

        public float GetWidth()
        {
            return width;
        }

        public void Setup()
        {
            this.gameObject.SetActive(true);
            components.ForEach(component =>
            {
                component.Setup();
            });
        }

        public void Setdown()
        {
            this.gameObject.SetActive(false);

            components.ForEach(component =>
            {
                component.Setdown();
            });
        }
    }

}
