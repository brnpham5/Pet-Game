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
        public ComponentCollection enemies;
        public ComponentCollection interactables;
        public ComponentCollection pickups;
        public ComponentCollection traps;
        

        public BoxCollider2D exitCollider;

        private float center;
        private float extent;
        private float width;

        

        private void Awake()
        {
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

            enemies.Setup();
            interactables.Setup();
            pickups.Setup();
            traps.Setup();
            
        }

        public void Setdown()
        {
            this.gameObject.SetActive(false);

            enemies.Setdown();
            interactables.Setdown();
            pickups.Setdown();
            traps.Setdown();
        }
    }

}
