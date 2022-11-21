using System;
using UnityEngine;

namespace AI.Goap
{
    [Serializable]
    public class GoapRequirement
    {
        [SerializeField] private string name;
        public GoapTask[] options;
        public GoapTask bestOption;
        public bool IsComplete(GoapAgent agent, GoapBlackboard blackboard) 
        { 
            foreach (GoapTask task in options) 
            { 
                if (task.IsComplete(agent,blackboard)) return true;
            } 
            return false;
        }
        
        public bool IsValid(GoapAgent agent, GoapBlackboard blackboard)
        {
            foreach (GoapTask task in options)
            {
                if (task.IsValid(agent,blackboard)) return true;
                
            } 
            return false;
        }
    }
}
