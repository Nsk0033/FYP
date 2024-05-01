using UnityEngine;
using UnityEngine.AI;

namespace UniBT.Examples.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public float Hate { get; private set; }
        public float Love { get; private set; }

        public bool Attacking { get; private set; }

        [SerializeField]
        private Transform player;

        public Transform enemy;
        
        private Rigidbody rigid;
        
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (enemy != null)
            {
                var enemydistance = Vector3.Distance(transform.position, enemy.position);

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
        }

        public void Attack(float force)
        {
            Attacking = true;
            navMeshAgent.enabled = false;
            rigid.isKinematic = false;
            rigid.AddForce(Vector3.up * force, ForceMode.Impulse);
        }

        private void OnCollisionStay(Collision other)
        {
            // TODO other.collider.name cause GC.Alloc by Object.GetName
            if (Attacking && other.collider.name == "Ground" && Mathf.Abs(rigid.velocity.y) < 0.1)
            {
                CancelAttack();
            }

            if (other.collider.tag == "Enemy")
            {
                enemy = other.transform;
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