using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner {
    public class PlatformStarter : MonoBehaviour {
        [Header("Scene Reference")]
        public PlatformSpawner spawner;
        
        [Header("Scriptable Reference")]
        public PlatformConfigData data;

        public void SpawnPlatforms() {
            float width = (int) (data.SpawnOffset + data.CameraOffset);
            if(width % 2 == 1)
            {
                width += 0.5f;
            }
            spawner.Spawn(width, -data.CameraOffset);
        }
    }

}
