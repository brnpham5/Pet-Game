using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InfiniteRunner
{
    [RequireComponent(typeof(ObstacleComponent))]
    public class SpiderController : MonoBehaviour
    {
        public Enemy enemy;
        public BehaviourSpider spider;
        public ObstacleComponent obstComp;
        public SpiderFlyController fly;
        public Stretchable web;

        private void Awake()
        {
            obstComp.OnSetup += Setup;
        }

        public void Setup()
        {
            enemy.Setup();
            fly.Setup();
            web.SetActive(true);
        }

        public void Death()
        {
            web.SetActive(false);
        }

        private void OnDestroy()
        {
            obstComp.OnSetup -= Setup;
        }
    }

}
