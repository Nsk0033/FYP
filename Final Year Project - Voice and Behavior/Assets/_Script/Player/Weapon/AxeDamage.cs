using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallHedge.SoundManager;

public class AxeDamage : MonoBehaviour
{
	[SerializeField] private GameObject HitVFX;
	[SerializeField] private GameObject BloodVFX;
	[SerializeField] private Transform spawnParticlePosition;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private Transform MainCharacter;
	[SerializeField] private GameObject Skill2Object;
	[SerializeField] private int damageOutput;
	//private PlayerActionPoint playerActionPoint;
	//private PlayerLimit playerLimit;
	
	private PlayerAnimationEventTrigger playerAnimationEventTrigger;
	
	private void Start()
	{
		playerAnimationEventTrigger = MainCharacter.GetComponent<PlayerAnimationEventTrigger>();
		//playerActionPoint = MainCharacter.GetComponent<PlayerActionPoint>();
		//playerLimit = MainCharacter.GetComponent<PlayerLimit>();
	}
	
	public void DealDamage()
    {
        GameEventsManager.instance.playerEvents.OnDealDamage.Invoke(damageOutput);
    }
	
    private void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Axe hit something!");
		
		// Log the name of the object that was hit
        Debug.Log("Object hit: " + collider.gameObject.name);
		
		if (collider.GetComponent<EmeraldAI.EmeraldAISystem>() != null)
		{
		   collider.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(damageOutput, EmeraldAI.EmeraldAISystem.TargetType.Player, MainCharacter, 400);
		   DealDamage();
		}
		
		if (collider.CompareTag("Breakable"))
        {
            if(collider.GetComponent<ObjectBreakingScript>() != null)
            {
                collider.GetComponent<ObjectBreakingScript>().Break();
            }
        }
		
		if (collider.CompareTag("Freezable"))
        {
			if(Skill2Object.activeSelf)
			{
				if(collider.GetComponent<ObjectFreezingScript>() != null)
				{
					collider.GetComponent<ObjectFreezingScript>().Freeze();
				}
			}
        }
		
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
				
				SoundType soundToPlay = SoundType.AXEDAMAGE;
				SoundManager.PlaySound(soundToPlay, null, 0.4f);
			}
		}
	}
}
