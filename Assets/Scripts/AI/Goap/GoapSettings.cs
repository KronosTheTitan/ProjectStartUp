using UnityEngine;

namespace AI.Goap
{
    [CreateAssetMenu(fileName = "new GOAP settings", menuName = "AI/GOAP/settings")]
    [ExecuteAlways]
    public class GoapSettings : ScriptableObject
    {
        public static GoapSettings Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                DestroyImmediate(this);
            }
        }
    
    
    }
}
