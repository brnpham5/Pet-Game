using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner {
    [CreateAssetMenu(fileName = "Gameplay Data", menuName = "Runner/Config/Gameplay Data")]
    public class GameplayData : ScriptableObject {
        public List<LevelData> levels;
    }
}