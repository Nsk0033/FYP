using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class SignalAction : Action
    {
        private float atkTimer;

        private Companion companion;

        private bool triggered = false;

        private Animator animator;
        public override void Awake()
        {
            companion = gameObject.GetComponent<Companion>();
            animator = gameObject.GetComponent<Animator>();
        }
        
        protected override Status OnUpdate()
        {
            atkTimer += Time.deltaTime;
            if (triggered)
            {
                if (companion.Attacking)
                {
                    if (atkTimer >= 5)
                    {
                        animator.SetTrigger("Attack1");
                        atkTimer = 0;
                        return Status.Running;
                    }
                }
                triggered = false;
                return Status.Success;
            }
            companion.Attack();
            triggered = true;
            return Status.Running;
        }
    }
}