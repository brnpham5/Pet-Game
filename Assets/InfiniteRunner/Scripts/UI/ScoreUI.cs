using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Core;

namespace InfiniteRunner
{
    public class ScoreUI : PanelController
    {
        public ScriptableInventory inventory;
        public TMP_Text scoreText;


        private void Start()
        {
            UpdateUI();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public void UpdateUI()
        {
            scoreText.text = inventory.score.GetValue().ToString();
        }

        private void OnEnable()
        {
            inventory.score.OnChange += UpdateUI;
            GameStartManager.OnStart += UpdateUI;
        }

        private void OnDisable()
        {
            inventory.score.OnChange -= UpdateUI;
            GameStartManager.OnStart -= UpdateUI;

        }
    }

}
