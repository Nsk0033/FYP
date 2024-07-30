using Codice.Client.BaseCommands.Merge.Xml;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class AttackAction : Action
    {

        [SerializeField]
        private int type;

        private float atkTimer;
        private float ultTimer;

        private Enemy enemy;

        private bool triggered = false;

        private Animator animator;

        //private NavMeshAgent navMeshAgent;
        public override void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
            //navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            enemy = gameObject.GetComponent<Enemy>();
        }
        
        protected override Status OnUpdate()
        {
            atkTimer += Time.deltaTime;
            ultTimer += Time.deltaTime;
            if (triggered)
            {

                if (enemy.Attacking)
                {
                    if (type == 1 && atkTimer >= 2)
                    {
                        animator.SetTrigger("Attack1");
                        atkTimer = 0;
                        return Status.Running;
                    }
                    else if (type == 2 && ultTimer >= 5) //(&&playerhp<?%)
                    {
                        animator.SetTrigger("Ult");
                        ultTimer = 0;
                        return Status.Running;
                    }
                }
                triggered = false;
                return Status.Success;
            }
            enemy.Attack();
            triggered = true;
            return Status.Running;
        }

        public override void Abort()
        {
            enemy.CancelAttack();
            triggered = false;
        }
    }
}