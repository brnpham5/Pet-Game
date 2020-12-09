using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SpritesheetAnimation : MonoBehaviour
    {
        [Header("Asset Reference")]
        public List<Sprite> sprites;

        [Header("GameObject Reference")]
        public SpriteRenderer sr;

        private void Awake()
        {
            if(sr == null)
            {
                sr = GetComponent<SpriteRenderer>();
            }
        }

        public void ChangeSprite(int index)
        {
            if (index < sprites.Count)
            {
                sr.sprite = sprites[index];
            }
        }
    }

}
