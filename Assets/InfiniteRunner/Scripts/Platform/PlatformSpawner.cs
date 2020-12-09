using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    public class PlatformSpawner : MonoBehaviour {
        [Header("Asset Reference")]
        public Platform_Basic platform_basic;

        [Header("Scriptable Reference")]
        public PlatformConfigData data;

        [Header("Runtime")]
        public List<Platform> pool;

        [Header("Scene Reference")]
        public GameObject platformPool;

        public void Setup() {

        }

        public void Setdown() {
            pool.ForEach(platform => {
                platform.Setdown();
            });

        }

        public Platform Spawn(float width, float pos) {
            Platform platform = GetPool();

            platform.Setup(width);

            PositionPlatform(platform, width, pos);

            return platform;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform">Platform object</param>
        /// <param name="width">Width of platform</param>
        /// <param name="pos">World x position</param>
        private void PositionPlatform(Platform platform, float width, float pos) {
            float modPos = pos + (width / 2);
            if(width % 2 == 1) {
                modPos -= 0.5f;
            }

            platform.transform.position = new Vector2(modPos, data.GroundLevel);
        }

        private Platform GetPool() {
            Platform item;

            for(int i = 0; i < pool.Count; i++) {
                item = pool[i];
                if(item.isActiveAndEnabled == false) {
                    return item;
                }
            }

            item = Instantiate(platform_basic);
            item.transform.parent = platformPool.transform;
            pool.Add(item);

            return item;
        }
    }

}
