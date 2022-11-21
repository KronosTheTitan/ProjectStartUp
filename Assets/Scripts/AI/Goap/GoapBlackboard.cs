using System;
using UnityEngine;

namespace AI.Goap
{
    [Serializable]
    public class GoapBlackboard
    {
        [SerializeField] private GoapAgent agent;
        
        public int temp;
    }
}
