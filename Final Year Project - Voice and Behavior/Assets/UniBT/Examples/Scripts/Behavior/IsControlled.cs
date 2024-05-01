using UnityEngine;

namespace UniBT.Examples.Scripts.Behavior
{
    public class IsControlled : Conditional
    {
        private bool isControlled;

        private Enemy enemy;
        
        protected override void OnAwake()
        {
            enemy = gameObject.GetComponent<Enemy>();
        }

        protected override bool IsUpdatable()
        {
            if (Input.GetMouseButtonDown(1))
            {
                isControlled = !isControlled;
            }
            return isControlled;
        }
    }
}