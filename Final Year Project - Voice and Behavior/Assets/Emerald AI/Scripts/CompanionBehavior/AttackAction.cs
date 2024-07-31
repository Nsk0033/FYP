using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class AttackAction : Action
    {

        [SerializeField]
        private int type;
        [SerializeField]
        private int damagevalue;
        [SerializeField]
        private GameObject punch;
        [SerializeField]
        private GameObject swipe;
        [SerializeField]
        private GameObject roll;

        private float atkTimer;
        private float ultTimer;
        private float rollTimer;

        private Companion companion;

        private bool triggered = false;

        private Animator animator;

        //private NavMeshAgent navMeshAgent;
        public override void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
            //navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            companion = gameObject.GetComponent<Companion>();
        }
        
        protected override Status OnUpdate()
        {
            atkTimer += Time.deltaTime;
            ultTimer += Time.deltaTime;
            rollTimer += Time.deltaTime;
            if (triggered)
            {

                if (companion.Attacking)
                {
                    if (type == 1 && atkTimer >= 2)
                    {
                        animator.SetTrigger("Attack1");
                        GameObject.Instantiate(punch, gameObject.transform.position, gameObject.transform.rotation);
                        DealDamage(damagevalue);
                        atkTimer = 0;
                        return Status.Running;
                    }
                    else if (type == 2 && ultTimer >= 5)
                    {
                        animator.SetTrigger("Ult");
                        GameObject.Instantiate(swipe, gameObject.transform.position, gameObject.transform.rotation);
                        DealDamage(damagevalue);
                        ultTimer = 0;
                        return Status.Running;
                    }
                    else if (type == 3 && rollTimer >= 10)
                    {
                        animator.SetTrigger("Roll");
                        GameObject.Instantiate(roll, gameObject.transform.position, gameObject.transform.rotation);
                        rollTimer = 0;
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

        public override void Abort()
        {
            companion.CancelAttack();
            triggered = false;
        }

        private void DealDamage (int damage)
        {
            if (companion.enemyTarget.GetComponent<EmeraldAI.EmeraldAISystem>() != null)
            {
                companion.enemyTarget.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(damagevalue, EmeraldAI.EmeraldAISystem.TargetType.AI, gameObject.transform, 400);
            }
        }
    }
}