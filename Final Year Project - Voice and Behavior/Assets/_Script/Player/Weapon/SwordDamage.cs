using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
	[SerializeField] private GameObject HitVFX;
	[SerializeField] private GameObject BloodVFX;
	[SerializeField] private Transform spawnParticlePosition;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private Transform MainCharacter;
	
	private PlayerAnimationEventTrigger playerAnimationEventTrigger;
	
	private void Start()
	{
		playerAnimationEventTrigger = MainCharacter.GetComponent<PlayerAnimationEventTrigger>();
	}
	
    private void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Sword hit something!");
		
		// Log the name of the object that was hit
        Debug.Log("Object hit: " + collider.gameObject.name);

		IDamageable damageable = collider.GetComponent<IDamageable>();
		{
			if (damageable != null)
			{
				damageable.Damage();
				playerAnimationEventTrigger.AnimationHitStop();
				GameObject hitvfx = Instantiate(HitVFX, spawnParticlePosition.position, Quaternion.identity);
				GameObject bloodvfx = Instantiate(BloodVFX, spawnParticlePosition.position, Quaternion.identity);
				
				// Calculate the direction from the particle's position to the player's position
				//Vector3 directionToPlayer = (playerTransform.position - spawnParticlePosition.position).normalized;  
				Vector3 directionAwayEnemy = -1*(collider.transform.position - spawnParticlePosition.position).normalized;

				// Set the particle system's forward direction
				hitvfx.transform.forward = directionAwayEnemy;
				bloodvfx.transform.forward = directionAwayEnemy;
			}
		}
	}
}
