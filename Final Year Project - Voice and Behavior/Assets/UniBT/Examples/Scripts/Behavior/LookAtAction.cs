using UnityEngine;

namespace UniBT.Examples.Scripts.Behavior
{
    public class LookAtAction : Action
    {
        [SerializeField]
        private bool useLerp = false;

        public Transform target;

        private Transform transform;

        Enemy enemy;

        public override void Awake()
        {
            transform = gameObject.transform;
            enemy = gameObject.GetComponent<Enemy>();
        }

        protected override Status OnUpdate()
        {
            target = enemy.followTarget;

            if (useLerp)
            {
                var from = transform.position;
                var direction = target.position- from;
                var goal = Quaternion.LookRotation(direction);
                if (Quaternion.Angle(goal, transform.rotation) < 5.0f)
                {
                    return Status.Success;
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, goal, Time.deltaTime * 2);
                return Status.Running;
            }
            
            transform.LookAt(target);
            return Status.Running;

        }

    }
}