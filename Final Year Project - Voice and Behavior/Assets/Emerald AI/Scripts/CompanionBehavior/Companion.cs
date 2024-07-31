using UniBT.Examples.Scripts.Behavior;
using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts
{
    public class Companion : MonoBehaviour
    {
        public float Hate { get; private set; }
        public float Love { get; private set; }

        public bool Attacking { get; private set; }

        public Transform followTarget;
        public Transform enemyTarget;

        [SerializeField]
        private Transform player;

        [SerializeField]
        private Transform respawn;

        private Rigidbody rigid;
        
        private NavMeshAgent navMeshAgent;

        FollowAction followAction;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            followAction = GetComponent<FollowAction>();
        }

        private void Update()
        {
            if (enemyTarget == null && followTarget == null)
            {
                Hate = 0;
                followTarget = player;
            }

            var playerdistance = Vector3.Distance(transform.position, player.position);
            if (playerdistance < 5)
            {
                Love = 20;
            }
            else if (playerdistance < 10)
            {
                Love = 9;
            }
            else
            {
                Love = 0;
            }
            
            if (enemyTarget != null)
            {
                followTarget = enemyTarget;

                var enemydistance = Vector3.Distance(transform.position, enemyTarget.position);
                if (enemydistance < 5)
                {
                    Hate = 20;
                }
                else if (enemydistance < 10)
                {
                    Hate = 9;
                }
                else
                {
                    Hate = 0;
                }
            }
            
            if (Input.GetKey(KeyCode.R))
            {
                navMeshAgent.Warp(respawn.position);
                enemyTarget = null;
                followTarget = null;
            }
        }

        public void Attack()
        {
            Attacking = true;
            //navMeshAgent.enabled = false;
            //rigid.isKinematic = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemyTarget = other.transform;
                followTarget = enemyTarget;
                Debug.Log("this is the enemy:" + enemyTarget);
            }
        }


        public void CancelAttack()
        {
            navMeshAgent.enabled = true;
            rigid.isKinematic = true;
            Attacking = false;
        }
        
    }
}