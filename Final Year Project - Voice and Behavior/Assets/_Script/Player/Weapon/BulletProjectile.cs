using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;
using SmallHedge.SoundManager;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
	[SerializeField] private float speed = 20f;
	[SerializeField] private GameObject vfxHit;
	[SerializeField] private int damageOutput = 2;
	[SerializeField] private SoundType sound;
	
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
		Destroy(gameObject,7f);
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
		var emeraldAI = other.GetComponent<EmeraldAI.EmeraldAISystem>();
        if (emeraldAI != null)
        {
            emeraldAI.Damage(damageOutput, EmeraldAI.EmeraldAISystem.TargetType.Player, transform, 400);
            DealDamage();
        }
		
		if(vfxHit != null)
		{
			GameObject hitvfx = Instantiate(vfxHit, transform.position, Quaternion.identity);
			Vector3 directionAwayEnemy = -1*(other.transform.position - transform.position).normalized;
			hitvfx.transform.forward = directionAwayEnemy;
			SoundManager.PlaySound(sound, null, 0.3f);
		}
        
        Destroy(gameObject);
		
    }
	
	public void DealDamage()
    {
        GameEventsManager.instance.playerEvents.OnDealDamage.Invoke(damageOutput);
        Debug.Log($"Skill Hit! Damage Output: {damageOutput} from {gameObject.name}");
    }
}
