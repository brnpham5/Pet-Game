using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace InfiniteRunner {
    /// <summary>
    /// Handles all logic regarding starting a game
    /// Reset position of player
    /// Fire event for all systems to reset values
    /// Reset score
    /// Start game
    /// </summary>
    public class GameStartManager : MonoBehaviour {
        public delegate void GameDelegate();
        public static event GameDelegate OnSetup;
        public static event GameDelegate OnStart;

        [Header("Scriptable reference")]
        public ScriptableInventory inventory;
        public SFXSet sfxBGM;

        private void Start() {
            Restart();
        }

        public void Restart() {
            sfxBGM.Play();
            inventory.energy.SetValue(10);
            inventory.health.SetValue(1);
            inventory.keyLrg.SetValue(0);
            inventory.keySml.SetValue(0);
            inventory.score.SetValue(0);
            OnSetup?.Invoke();
            OnStart?.Invoke();
        }

        private void OnEnable() {
            GameEndManager.OnGameRestart += Restart;
        }

        private void OnDisable() {
            GameEndManager.OnGameRestart -= Restart;
        }
    }

}
