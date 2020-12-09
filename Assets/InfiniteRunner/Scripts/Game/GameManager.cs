using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    /// <summary>
    /// Handles logic during gameplay
    /// </summary>
    public class GameManager : MonoBehaviour {
        public delegate void GameDelegate();
        public static event GameDelegate OnSetLevel;
        public static event GameDelegate OnZero;
        public static event GameDelegate OnReady;

        [Header("Scriptable Reference")]
        public CurrentLevelData currentLevel;
        public GameplayData gameplayData;
        public PlatformConfigData platformConfig;

        [Header("Scene Reference")]
        public Player player;
        public Camera mainCamera;

        private int currentIndex;

        private void Start() {
            if(mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        public void ResetPosition()
        {
            OnZero?.Invoke();
            player.transform.position = Vector3.zero;
            mainCamera.transform.position = new Vector3(platformConfig.CameraOffset, mainCamera.transform.position.y, mainCamera.transform.position.z);
            OnReady?.Invoke();
        }

        public void ResetXPosition()
        {
            OnZero?.Invoke();
            player.transform.position = new Vector3(0, player.transform.position.y, 0);
            mainCamera.transform.position = new Vector3(platformConfig.CameraOffset, mainCamera.transform.position.y, mainCamera.transform.position.z);
            OnReady?.Invoke();
        }

        public void StartGame()
        {
            ResetPosition();
        }

        /// <summary>
        /// Initialize first level
        /// </summary>
        public void Setup() {
            currentIndex = 0;
            SetLevel(0);
        }

        /// <summary>
        /// Increment current level and set next level into scriptable object
        /// </summary>
        public void NextLevel() {
            currentIndex++;
            SetLevel(currentIndex);
        }

        /// <summary>
        /// Sets the level into the scriptable object and fires Set Level event
        /// </summary>
        /// <param name="index"></param>
        public void SetLevel(int index) {
            currentLevel.data = gameplayData.levels[index];
            OnSetLevel?.Invoke();
        }

        private void OnEnable() {
            GameStartManager.OnSetup += Setup;
            GameStartManager.OnStart += StartGame;
            Resetter.OnReset += ResetXPosition;
        }

        private void OnDisable() {
            GameStartManager.OnSetup -= Setup;
            GameStartManager.OnStart -= StartGame;
            Resetter.OnReset -= ResetXPosition;
        }
    }

}
