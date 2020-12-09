using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner
{
    public class SpawnManager : MonoBehaviour
    {

        [Header("Scriptable Reference")]
        public PlatformConfigData platformData;

        [Header("Scene Reference")]
        public Camera mainCamera;
        public ObstacleSpawner obstSpawn;
        public PlatformSpawner platSpawn;
        public PlatformStarter platStart;
        public DestroyZone destroyZone;

        private bool active = false;
        private bool spawnGround = true;
        public int currentPosition;
        public int nextPosition;
        private float interval;
        private float posY;
        private float posZ;
        private WaitForFixedUpdate wait = new WaitForFixedUpdate();

        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            this.posY = this.transform.position.y;
            this.posZ = this.transform.position.z;
        }

        public void Setup()
        {
            currentPosition = (int)platformData.SpawnOffset;
            nextPosition = currentPosition;

            platStart.SpawnPlatforms();
            obstSpawn.Setup();
            platSpawn.Setup();

            this.transform.position = new Vector3(mainCamera.transform.position.x + platformData.SpawnOffset, this.posY, this.posZ);
        }

        public void Setdown()
        {
            active = false;
            interval = 0;

            obstSpawn.Setdown();
            platSpawn.Setdown();
        }

        public void ResetGround()
        {
            StopAllCoroutines();
            Setdown();
            Setup();
        }

        public void StartGame()
        {
            active = true;
            StopAllCoroutines();
            StartCoroutine(FixedUpdateCoroutine());
        }

        /// <summary>
        /// Check when to spawn a new obstacle, then spawn it
        /// </summary>
        /// <returns></returns>
        public IEnumerator FixedUpdateCoroutine()
        {
            ObstacleTilemap obst;
            while (active)
            {
                yield return wait;
                //Move spawner according to camera position
                this.transform.position = new Vector3(mainCamera.transform.position.x + platformData.SpawnOffset, this.posY, this.posZ);

                //Check if current position has passed the next position to spawn the obstacle
                if (this.transform.position.x > nextPosition)
                {
                    currentPosition = nextPosition;

                    //If past the next position, spawn the obstacle
                    if (interval <= 0)
                    {
                        obst = SpawnObstacle();

                        //Set the interval until next obstacle spawn
                        interval = obst.GetWidth();

                        //Spawn the ground (interval is the width of the ground gameObject)
                        if (spawnGround == true)
                        {
                            //Offset by value after decimal
                            float offset = -obst.GetCenter() % 1;
                            SpawnGround(interval, offset);
                        }
                    }

                    nextPosition += 1;

                    interval--;
                }
            }
        }

        public void SpawnResetter()
        {
            obstSpawn.SetupResetObst();
        }

        public ObstacleTilemap SpawnObstacle()
        {
            ObstacleTilemap obst = obstSpawn.SpawnNext();
            obst.Setup();
            obstSpawn.PositionObstacle(obst, currentPosition + obst.GetExtent());
            return obst;
        }

        public Platform SpawnGround(float width, float offset)
        {
            return platSpawn.Spawn(width, currentPosition + offset);
        }

        private void OnEnable()
        {
            GameStartManager.OnSetup += Setup;
            GameStartManager.OnStart += StartGame;
            GameManager.OnZero += ResetGround;
            GameManager.OnReady += StartGame;
            GameEndManager.OnGameEnd += Setdown;
            Player.OnOutOfBounds += SpawnResetter;
        }

        private void OnDisable()
        {

            GameStartManager.OnSetup -= Setup;
            GameStartManager.OnStart -= StartGame;
            GameManager.OnZero -= ResetGround;
            GameManager.OnReady -= StartGame;
            GameEndManager.OnGameEnd -= Setdown;
            Player.OnOutOfBounds -= SpawnResetter;

        }
    }

}
