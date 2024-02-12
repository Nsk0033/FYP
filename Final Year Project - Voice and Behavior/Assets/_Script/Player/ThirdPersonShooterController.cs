using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
	//[SerializeField] private Rig aimRig;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
	[SerializeField] private float normalSensitivity;
	[SerializeField] private float aimSensitivity;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	[SerializeField] private Transform debugTransform;
	[SerializeField] private Transform pfBulletProjectile;
	[SerializeField] private Transform spawnBulletPosition;
	[SerializeField] private GameObject crosshairCanva;
	//[SerializeField] private int meleeCounter = 1;
	
	[Header("Character Melee Attack")]
	[SerializeField] private float MeleeAttackCD = 0.5f;
	[SerializeField] private bool canMeleeAttack = true;
	[SerializeField] private bool hasMeleeAttacked;
	
	[Header("Character Charged Attack")]
	[SerializeField] private float chargedAttackTimeNeeded = 0.5f;
	[SerializeField] private float chargedAttackDistance = 3f;
	
	[Header("Character Ranged Attack")]
	[SerializeField] public int bulletCount = 5;
    [SerializeField] public float spreadAngle = 30f;
	[SerializeField] private float RangeAttackCD = 0.3f;
	[SerializeField] private bool canRangeAttack = true;
	
	[Header("Character Weapon")]
	[SerializeField] private GameObject bowGameObject;
	[SerializeField] private GameObject swordGameObject;
	
	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
	private float aimRigWeight;
    private float rangeLastUsedTime;
	private float meleeLastUsedTime;
	
	
	private void Awake()
	{
		
		thirdPersonController = GetComponent<ThirdPersonController>();
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		animator = GetComponent<Animator>();
		
	}
	
	private void Start()
	{
		meleeLastUsedTime = -MeleeAttackCD;
		rangeLastUsedTime = -RangeAttackCD;
		bowGameObject.SetActive(false);
		swordGameObject.SetActive(false);
	}
	
	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		
		canMeleeAttack = Time.time - meleeLastUsedTime > MeleeAttackCD; //melee attack cooldown check
		canRangeAttack = Time.time - rangeLastUsedTime > RangeAttackCD; //melee attack cooldown check
		
		
		
		if (Physics.Raycast(ray,out RaycastHit raycastHit, 999f, aimColliderLayerMask))
		{
			debugTransform.position = raycastHit.point;
			mouseWorldPosition = raycastHit.point;
			if (raycastHit.collider.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
			{
				//rigidbody.AddExplosionForce(1000f,targetPosition,5f);
			}
		}
		else
        {
            mouseWorldPosition = ray.GetPoint(10);
            debugTransform.position = ray.GetPoint(10);
        }
		
		if (starterAssetsInputs.aim)
		{
			animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
			swordGameObject.SetActive(false);
			bowGameObject.SetActive(true);
			crosshairCanva.SetActive(true);
			aimVirtualCamera.gameObject.SetActive(true);
			thirdPersonController.SetSensitivity(aimSensitivity);
			thirdPersonController.SetRotateOnMove(false);
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
			
			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
			
			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
			
			//aimRig.weight = 1f;
			
		}
		else
		{
			animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0.8f, Time.deltaTime * 10f));
			animator.ResetTrigger("MeleeAttack");
			animator.ResetTrigger("ChargedAttack");
			//swordGameObject.SetActive(true);
			bowGameObject.SetActive(false);
			crosshairCanva.SetActive(false);
			aimVirtualCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
			thirdPersonController.SetRotateOnMove(true);
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
			//aimRig.weight = 0f;
			
			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
			
			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
			
			if(canMeleeAttack)
			{
				//animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
				hasMeleeAttacked = false;
				
			}
			else
			{
				if(!hasMeleeAttacked)
				{
					thirdPersonController.SetRotateOnMove(true);
					//animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 200f));
					
					/*Vector3 worldAimTarget = mouseWorldPosition;
					worldAimTarget.y = transform.position.y;
					Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
					
					transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 50f);*/
					//Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
					
					hasMeleeAttacked = true;
				}
			}
		}
	
		
		if (starterAssetsInputs.shoot)
		{
			if(starterAssetsInputs.aim)
			{
				if(canRangeAttack)
				{
					//if can range attack but not attacking then do this
					animator.SetTrigger("Recoil");
					Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
					Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
					for (int i = 0; i < bulletCount; i++)
					{
						float Yangle = Random.Range(-spreadAngle, spreadAngle);
						float Xangle = Random.Range((-spreadAngle*0.5f), (spreadAngle*0.5f));
						Quaternion rotation = Quaternion.Euler(Xangle, Yangle, 0f);

						Vector3 aimSpreadDir = rotation * aimDir;
						Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimSpreadDir, Vector3.up));
					}
					starterAssetsInputs.shoot = false;
					rangeLastUsedTime = Time.time;
					// if attacking then do ntg
					
				}
				else
					return;
			}
			else
			{
				if (canMeleeAttack)
				{				
					meleeLastUsedTime = Time.time;
					animator.SetTrigger("MeleeAttack");	
					starterAssetsInputs.shoot = false;
					Debug.Log("Attack");
				}
			}
		}	
		
		if (starterAssetsInputs.chargedAttack)
		{
			// Trigger the charged attack animation
			animator.SetTrigger("ChargedAttack");

			// Move the character forward gradually
			StartCoroutine(LerpCharacterForward());

			// Reset the chargedAttack state
			starterAssetsInputs.chargedAttack = false;
		}
	}
	
	IEnumerator LerpCharacterForward()
	{
		Vector3 startPosition = transform.position;
		Vector3 targetPosition = transform.position + transform.forward * chargedAttackDistance;
		float duration = chargedAttackTimeNeeded; // Desired duration for the lerp (in seconds)
		float startTime = Time.time;

		// Add a Rigidbody component if not already present
		Rigidbody rb = GetComponent<Rigidbody>();

		while (Time.time - startTime < duration)
		{
			float t = (Time.time - startTime) / duration;
			Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);

			// Apply the movement to the character's Rigidbody
			if (rb)
			{
				// Use Rigidbody.MovePosition to move the character's Rigidbody
				rb.MovePosition(newPosition);
			}
			else
			{
				// If Rigidbody component is not present, directly set the position
				transform.position = newPosition;
			}

			yield return null;
		}

		// Ensure that the character ends up at the correct position
		if (rb)
		{
			// Use Rigidbody.MovePosition to move the character's Rigidbody
			rb.MovePosition(targetPosition);
		}
		else
		{
			transform.position = targetPosition;
		}

		// Trigger the jump action
		starterAssetsInputs.jump = true;
	}
		
	private void OnAttackStart(AnimationEvent animationEvent)
	{	
		swordGameObject.SetActive(true);
		Debug.Log("Show Sword");
		
	}
	
	private void OnAttackEnd(AnimationEvent animationEvent)
	{

		swordGameObject.SetActive(false);
		Debug.Log("Hide Sword");
		
	}
	
	private void OnRangedAttackEnd(AnimationEvent animationEvent)
	{

		animator.ResetTrigger("Recoil");
		Debug.Log("Stop Furthure Shooting");
	}
	
}
