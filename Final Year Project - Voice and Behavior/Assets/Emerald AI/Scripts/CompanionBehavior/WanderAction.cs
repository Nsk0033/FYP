using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts.Behavior
{
    public class WanderAction : Action
    {

        private static readonly int Walking = Animator.StringToHash("Walking");
        
        [SerializeField] 
        private Transform target;

        [SerializeField] 
        private float speed;

        [SerializeField]
        private float stoppingDistance;

        private Animator animator;

        private NavMeshAgent navMeshAgent;

        [SerializeField]
        public float wanderArea = 3f;
        [SerializeField]
        public float wanderTime = 2f;

        private float timer;
        private float wanderCheckTime;
        public override void Awake()
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            animator = gameObject.GetComponent<Animator>();
        }

        public override void Start()
        {
            timer = wanderTime;
            Wander();
        }

        protected override Status OnUpdate()
        {
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stoppingDistance;

            timer += Time.deltaTime;

            if (timer >= wanderTime)
            {
                Wander();
                timer = 0;
            }

            if (IsDone)
            {
                SetWalking(false);
                return Status.Success;
            }

            SetWalking(true);
            return Status.Running;


        }
        private void Wander()
        {
            //Vector3 newPos = RandomNavSphere(target.position, wanderArea, -1);
            //navMeshAgent.SetDestination(newPos);
        }

        public override void Abort()
        {
            SetWalking(false);
        }

        private void SetWalking(bool walking)
        {
            if (animator != null)
            {
                animator.SetBool(Walking, walking);
            }

            navMeshAgent.isStopped = !walking;
        }

        private bool IsDone => !navMeshAgent.pathPending &&
                               (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance ||
                                Mathf.Approximately(navMeshAgent.remainingDistance, navMeshAgent.stoppingDistance));
    }
}