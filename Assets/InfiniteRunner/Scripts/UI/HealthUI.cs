using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Core;


namespace InfiniteRunner
{
    public class HealthUI : PanelController
    {
        [Header("Scriptable Reference")]
        public ScriptableInventory inventory;

        [Header("Asset Reference")]
        public Sprite heart_1;
        public Sprite heart_2;
        public Sprite heart_3;

        [Header("Runtime")]
        public List<Image> images;

        public void UpdateUI()
        {
            SetHearts();
        }

        public void SetHearts()
        {
            //Calculate how many full hearts
            int full = Mathf.FloorToInt(inventory.health.GetValue() / 3);

            if (full >= images.Count)
            {
                full = images.Count;
                SetFullHearts(full);
            } else {
                SetFullHearts(full);
                SetSmallHearts(full);
                HideHearts(full);
            }

        }

        /// <summary>
        /// Sets all the full hearts up to index
        /// </summary>
        private void SetFullHearts(int index)
        {
            for (int i = 0; i < index; i++)
            {
                if (images[i].gameObject.activeSelf == false)
                {
                    images[i].gameObject.SetActive(true);
                }
                images[i].sprite = heart_3;
            }
        }

        private void SetSmallHearts(int index)
        {
            //Calcualte how many smaller heart
            int sml = inventory.health.GetValue() % 3;

            //Set smaller heart
            if (sml == 2)
            {
                if (images[index].gameObject.activeSelf == false)
                {
                    images[index].gameObject.SetActive(true);
                }
                images[index].sprite = heart_2;
            }
            else if (sml == 1)
            {
                if (images[index].gameObject.activeSelf == false)
                {
                    images[index].gameObject.SetActive(true);
                }
                images[index].sprite = heart_1;
            }
            else if (sml == 0)
            {
                images[index].gameObject.SetActive(false);
            }
        }

        private void HideHearts(int index)
        {
            //Hide the rest
            int theRest = index + 1;
            for (int i = theRest; i < images.Count; i++)
            {
                images[i].gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            inventory.health.OnChange += UpdateUI;
        }

        private void OnDisable()
        {
            inventory.health.OnChange -= UpdateUI;
        }
    }

}
