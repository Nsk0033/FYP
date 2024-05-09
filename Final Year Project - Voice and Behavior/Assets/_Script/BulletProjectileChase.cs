using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectileChase : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    [SerializeField] private float speed = 20f;
    [SerializeField] private GameObject vfxHit;
    [SerializeField] private string enemyTag; // Change to string for enemy tag
    [SerializeField] private float detectionRadius = 10f;
	[SerializeField] private float rotateSpeed = 5f;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void FixedUpdate()
    {
        Destroy(gameObject, 15f);
        ChaseTarget();
    }

    private void ChaseTarget()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
		Debug.Log("Number of enemies detected: " + colliders.Length);
		if (colliders.Length > 0)
		{
			// Find the closest enemy
			Transform closestEnemy = null;
			float closestDistance = float.MaxValue;
			foreach (Collider collider in colliders)
			{
				if (collider.CompareTag(enemyTag)) // Check tag instead of layer
				{
					float distance = Vector3.Distance(transform.position, collider.transform.position);
					if (distance < closestDistance)
					{
						closestDistance = distance;
						closestEnemy = collider.transform;
					}
				}
			}

			if (closestEnemy != null)
			{
				// Rotate towards the closest enemy
				Vector3 targetDirection = (closestEnemy.position - transform.position).normalized;
				Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
				
				// Smoothly rotate towards the target direction
				Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
				bulletRigidbody.MoveRotation(newRotation);

				// Move the bullet forward in its local forward direction
				bulletRigidbody.velocity = transform.forward * speed;
			}
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the "Projectile" tag
        if (other.CompareTag("Projectile"))
        {
            // Do nothing if the tag is "Projectile"
            return;
        }
		if (other.GetComponent<IgnoreHit>() != null)
		{
			return;
		}
		GameObject hitvfx = Instantiate(vfxHit, transform.position, Quaternion.identity);
        Vector3 directionAwayEnemy = -1*(other.transform.position - transform.position).normalized;
		hitvfx.transform.forward = directionAwayEnemy;
        Destroy(gameObject);
	}
}
