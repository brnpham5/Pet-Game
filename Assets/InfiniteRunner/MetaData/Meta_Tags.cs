using System.Collections;
using System.Collections.Generic;

namespace InfiniteRunner
{
    public static class Meta_Tags
    {
        public enum tags {
            Ground,
            Obstacle,
            Trap,
            Enemy,
            Floor,
            Interactable,
            Player,
            Pickup,
            Unlockable,
            FloatStop
        }

        public static string ground = "Ground";
        public static string obstacle = "Obstacle";
        public static string trap = "Trap";
        public static string enemy = "Enemy";
        public static string floor = "Floor";
        public static string floatStop = "FloatStop";
        public static string interactable = "Interactable";
        public static string player = "Player";
        public static string pickup = "Pickup";
        public static string unlockable = "Unlockable";
    }


}
