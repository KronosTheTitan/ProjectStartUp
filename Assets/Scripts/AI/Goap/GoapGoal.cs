using UnityEngine;
using System.Collections.Generic;

namespace AI.Goap
{
    public class GoapGoal : MonoBehaviour
    {
        [SerializeField] private GoapRequirement[] requirements;
        
        public virtual float CalculateGoalUtility(GoapAgent agent, GoapBlackboard blackboard)
        {
            return 1;
        }

        public virtual bool IsGoalCompleted(GoapAgent agent, GoapBlackboard blackboard)
        {
            return true;
        }

        public virtual bool IsValid(GoapAgent agent, GoapBlackboard blackboard)
        {
            foreach (GoapRequirement requirement in requirements)
            {
                if (!requirement.IsValid(agent,blackboard)) return false;
            }
            return true;
        }
        
        public GoapTask[] GetUncompletedRequirements(GoapAgent agent, GoapBlackboard blackboard)
        {
            List<GoapTask> uncompletedRequirements = new List<GoapTask>();

            foreach (GoapRequirement requirement in requirements)
            {
                if(requirement.IsComplete(agent,blackboard)) continue;
                
                float highestUtility = float.NegativeInfinity;
                
                foreach (GoapTask option in requirement.options)
                {
                    if (option.CalculateUtilityValue(agent, blackboard) > highestUtility && option.IsValid(agent,blackboard))
                    {
                        requirement.bestOption = option;
                    }
                }

                if (requirement.bestOption != null)
                {
                    uncompletedRequirements.AddRange(requirement.bestOption.GetUncompletedRequirements(agent,blackboard));
                }
            }

            return uncompletedRequirements.ToArray();
        }
    }
}
