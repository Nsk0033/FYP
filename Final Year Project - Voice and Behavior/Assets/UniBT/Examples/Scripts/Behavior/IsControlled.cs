using UnityEngine;

namespace UniBT.Examples.Scripts.Behavior
{
    public class IsControlled : Conditional
    {
        public bool isControlled = true;

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