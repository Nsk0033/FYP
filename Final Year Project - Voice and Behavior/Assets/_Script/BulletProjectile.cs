using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
	[SerializeField] private float speed = 20f;
	[SerializeField] private Transform vfxHit;
	
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
		Destroy(gameObject,15f);
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
        Instantiate(vfxHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
