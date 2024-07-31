using UnityEngine;

namespace UniBT.Examples.Scripts.Behavior
{
    public class IsHateGt : Conditional
    {
        
        [SerializeField] 
        private float threshold = 10;
        
        Companion companion;
        
        protected override void OnAwake()
        {
            companion = gameObject.GetComponent<Companion>();
        }

        protected override bool IsUpdatable()
        {
            return companion.Hate > threshold;
        }
    }
}