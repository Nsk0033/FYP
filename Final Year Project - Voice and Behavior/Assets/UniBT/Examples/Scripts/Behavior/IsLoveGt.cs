using UnityEngine;

namespace UniBT.Examples.Scripts.Behavior
{
    public class IsLoveGt : Conditional
    {
        
        [SerializeField] 
        private float threshold = 10;
        
        private Enemy enemy;
        
        protected override void OnAwake()
        {
            enemy = gameObject.GetComponent<Enemy>();
        }

        protected override bool IsUpdatable()
        {
            return enemy.Love > threshold;
        }
    }
}