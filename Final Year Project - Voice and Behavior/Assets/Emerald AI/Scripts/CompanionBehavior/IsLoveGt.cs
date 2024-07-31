using UnityEngine;

namespace UniBT.Examples.Scripts.Behavior
{
    public class IsLoveGt : Conditional
    {
        
        [SerializeField] 
        private float distance = 10;
        
        Companion companion;
        
        protected override void OnAwake()
        {
            companion = gameObject.GetComponent<Companion>();
        }

        protected override bool IsUpdatable()
        {
            return companion.Love > distance;
        }
    }
}