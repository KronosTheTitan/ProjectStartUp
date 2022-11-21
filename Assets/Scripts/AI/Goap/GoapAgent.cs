using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI.Goap
{
    public class GoapAgent : MonoBehaviour
    {
        /// <summary>
        /// This is an array of potential goals that the AI might want to execute.
        /// The selection for what goal to pick is based on a utility AI system.
        /// </summary>
        [SerializeField] private GoapGoal[] potentialGoals;
    
        /// <summary>
        /// The current goal the AI is trying to execute.
        /// </summary>
        [SerializeField] private GoapGoal currentGoal;

        /// <summary>
        /// This list contains the current plan.
        /// It will shrink and grow based on the completion and
        /// requirements of the goal and task.
        /// </summary>
        [SerializeField] private List<GoapTask> currentPlan;

        [SerializeField] private GoapBlackboard blackboard;
        
        /// <summary>
        /// This method sets the current goal.
        /// It does this by taking all the potential goals
        /// Checking which has the highest value and can be performed.
        /// </summary>
        private void SelectCurrentGoal()
        {
            //this list keeps track of all the goals that passed each
            //stage, it shrinks over time as goals are removed either because
            //they were invalid or because other goals with higher values existed.
            List<GoapGoal> filteredGoals = potentialGoals.ToList();

            //set up the highest utility value, initially it is at
            //the lowest possible because a bad plan is better than
            //no plan at all. It wouldn't be much fun if the AI never did anything.
            float highestUtility = Mathf.NegativeInfinity;

            //this stores the utility values for each goals so they don't have to be recalculated every time.
            Dictionary<GoapGoal, float> goalUtilityValues = new Dictionary<GoapGoal, float>();

            //loop over each potential goal
            foreach (GoapGoal goal in potentialGoals)
            {
                //if a goal is invalid for whatever reason remove it from the list.
                if (!goal.IsValid(this,blackboard))
                {
                    filteredGoals.Remove(goal);
                    continue;
                }

                //get the utility value for the goal
                float utility = goal.CalculateGoalUtility(this,blackboard);
                
                //if the utility of this goals is lower than the
                //highest utility remove it from circulation
                if (utility < highestUtility)
                {
                    filteredGoals.Remove(goal);
                    continue;
                }
               
                //change the highest utility to utility
                highestUtility = utility;

                goalUtilityValues.Add(goal,utility);
            }

            //this quits the function if no goals remain.
            //this situation only occurs if all goals are invalid.
            //in that case no goal will be set. And the AI will remain idle.
            if (filteredGoals.Count == 0)
            {
                return;
            }

            int i = 0;

            //remove remaining goals that slipped through the cracks in the first
            //loop. This can happen if they were accepted through on a lower treshold
            //than the end treshold.
            while (i < filteredGoals.Count)
            {
                GoapGoal goal = filteredGoals[i];
                if (goalUtilityValues[goal] < highestUtility)
                {
                    filteredGoals.Remove(goal);
                }
                else
                {
                    i++;
                }
            }

            //set the final goal.
            currentGoal = filteredGoals[Random.Range(0, filteredGoals.Count - 1)];
        }

        private void Update()
        {
            bool currentGoalIsValid = !(currentGoal == null || !currentGoal.IsValid(this, blackboard));

            if (!currentGoalIsValid || currentGoal.IsGoalCompleted(this,blackboard))
            {
                SelectCurrentGoal();
                if(currentGoal == null) return;

                currentPlan.Clear();
                currentPlan.AddRange(currentGoal.GetUncompletedRequirements(this,blackboard));
            }

            foreach (GoapTask task in currentPlan)
            {
                if(!task.IsReady(this,blackboard)) continue;
            }
        }
    }
}
