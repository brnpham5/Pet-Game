using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    public class ReSkinAnimation : MonoBehaviour
    {
        public string spriteSheetName;

        private void LateUpdate()
        {
            var subSprites = Resources.LoadAll<Sprite>("Characters/" + spriteSheetName);

            foreach(var renderer in GetComponentsInChildren<SpriteRenderer>())
            {
                string spriteName = renderer.sprite.name;

                var newSprite = Array.Find(subSprites, ContextMenuItemAttribute => ContextMenuItemAttribute.name == spriteName);

                if (newSprite)
                {
                    renderer.sprite = newSprite;
                }
            }
        }
    }

}
