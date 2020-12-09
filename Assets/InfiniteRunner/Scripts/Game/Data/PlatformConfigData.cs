using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner {
    [CreateAssetMenu(fileName = "Platform Config", menuName = "Runner/Config/Platform Config")]
    public class PlatformConfigData : ScriptableObject {
        [Header("Camera")]
        [Range(0, 10)]
        public float CameraOffset;

        [Range(0, 1)]
        public float CameraLerpValue;

        [Header("Platform Configs")]
        public float SpawnOffset;
        public float DestroyOffset;

        public float MaxHeight;
        public float GroundLevel;
        public float DeathLevel;
    }
}

