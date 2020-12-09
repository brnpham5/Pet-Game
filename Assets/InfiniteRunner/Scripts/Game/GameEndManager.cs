using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    /// <summary>
    /// Handles all logic regarding ending a game
    /// Fire game end event
    /// Save scores
    /// Apply experience
    /// Wait for player action
    /// Restart/Quit Minigame
    /// </summary>
    public class GameEndManager : MonoBehaviour {
        public delegate void GameDelegate();
        public static event GameDelegate OnGameEnd;
        public static event GameDelegate OnGameRestart;

        public ScriptableInventory inventory;

        private void Start() {
            
        }

        public void EndGame() {
            inventory.energy.SetValue(0);
            inventory.health.SetValue(0);
            inventory.keyLrg.SetValue(0);
            inventory.keySml.SetValue(0);
            inventory.score.SetValue(0);
            OnGameEnd?.Invoke();

            OnGameRestart?.Invoke();
        }

        private void OnEnable() {
            LoseArea.OnLose += EndGame;
        }

        private void OnDisable() {
            LoseArea.OnLose -= EndGame;
        }
    }

}
