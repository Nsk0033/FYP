using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class HealAction : Action
    {

        [SerializeField]
        private int type;
        [SerializeField]
        private int healvalue;
        [SerializeField]
        private GameObject smallheal;
        [SerializeField]
        private GameObject ultheal;

        private float atkTimer;
        private float ultTimer;

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
            if (triggered)
            {

                if (companion.Attacking)
                {
                    if (type == 1 && atkTimer >= 3 && companion.player.GetComponent<PlayerHealth>().CurrentHealth < 300)
                    {
                        animator.SetTrigger("Attack1");
                        Heal(healvalue);
                        GameObject.Instantiate(smallheal, companion.player.position, companion.player.rotation);
                        atkTimer = 0;
                        return Status.Running;
                    }
                    else if (type == 2 && ultTimer >= 30 && companion.player.GetComponent<PlayerHealth>().CurrentHealth <= 150)
                    {
                        animator.SetTrigger("Ult");
                        GameObject.Instantiate(ultheal, gameObject.transform.position, gameObject.transform.rotation);
                        Heal(healvalue);
                        ultTimer = 0;
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

        private void Heal (int heal)
        {
            companion.player.GetComponent<PlayerHealth>().HealPlayer(heal);
        }
    }
}