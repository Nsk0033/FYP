using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitParticle : MonoBehaviour
{
    [SerializeField] private GameObject HitVFX;

    private void OnCollisionEnter(Collision collision)
    {
		Debug.Log("On HIT!");
		IDamageable damageable = GetComponent<Collider>().GetComponent<IDamageable>();
		{
			// Check if the collision has contact points
			if (collision.contacts.Length > 0)
			{
				Debug.Log("Spawn HitVFX");
				// Get the first contact point
				ContactPoint contact = collision.contacts[0];
				// Spawn the object at the contact point
				Instantiate(HitVFX, contact.point, Quaternion.identity);
			}
		}
        
    }
}
