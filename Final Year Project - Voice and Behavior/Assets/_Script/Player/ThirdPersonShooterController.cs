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
	[Header("Character Component")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
	[SerializeField] private MonoBehaviour leaningAnimator;

	//[SerializeField] private int meleeCounter = 1;
	
	[Header("Character Melee Attack")]
	[SerializeField] private float MeleeAttackCD = 0.5f;
	[SerializeField] private bool canMeleeAttack = true;
	[SerializeField] private bool meleeLayer = true;
	
	[Header("Character Charged Attack")]
	[SerializeField] private float ChargedAttackCD = 2f;
	[SerializeField] private bool canChargedAttack = true;
	[SerializeField] private float chargedAttackTimeNeeded = 0.5f;
	[SerializeField] private float chargedAttackDistance = 3f;
	
	[Header("Character Ranged Attack")]
	[SerializeField] public int bulletCount = 5;
    [SerializeField] public float spreadAngle = 30f;
	[SerializeField] private float RangeAttackCD = 0.3f;
	[SerializeField] private bool canRangeAttack = true;
	[SerializeField] private float normalSensitivity;
	[SerializeField] private float aimSensitivity;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	[SerializeField] private Transform debugTransform;
	[SerializeField] private Transform pfBulletProjectile;
	[SerializeField] private Transform spawnBulletPosition;
	[SerializeField] private GameObject crosshairCanva;
	
	[Header("Character Weapon")]
	[SerializeField] private GameObject bowGameObject;
	[SerializeField] private GameObject swordGameObject;
	
	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
	private float aimRigWeight;
    private float rangeLastUsedTime;
	private float meleeLastUsedTime;
	private float chargedLastUsedTime;
	
	
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
		chargedLastUsedTime = -ChargedAttackCD;
		bowGameObject.SetActive(false);
		swordGameObject.SetActive(false);
		if (leaningAnimator == null)
        {
            Debug.LogError("Please assign a script to control.");
            return;
        }
		animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0.8f, Time.deltaTime * 10f));
	}
	
	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		
		canMeleeAttack = Time.time - meleeLastUsedTime > MeleeAttackCD; //melee attack cooldown check
		canRangeAttack = Time.time - rangeLastUsedTime > RangeAttackCD; //ranged attack cooldown check
		canChargedAttack = Time.time - chargedLastUsedTime > ChargedAttackCD; //charged attack cooldown check
		
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
			leaningAnimator.enabled = false;
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
			leaningAnimator.enabled = true;
			
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
			
			if(meleeLayer)
			{
				animator.SetLayerWeight(2, 0.8f);	
			}
			
			if(!canMeleeAttack)
			{
				//animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
				//hasMeleeAttacked = false;
				Vector3 worldAimTarget = mouseWorldPosition;
				worldAimTarget.y = transform.position.y;
				Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
				
				transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
			}
			/*else
			{
				if(!hasMeleeAttacked)
				{
					thirdPersonController.SetRotateOnMove(true);
					//animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 200f));
					
					Vector3 worldAimTarget = mouseWorldPosition;
					worldAimTarget.y = transform.position.y;
					Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
					
					transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 50f);
					//Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
					
					
					hasMeleeAttacked = true;
				}
				
				
			}*/
			

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
			if (starterAssetsInputs.aim)
			{
				return;
			}
			else
			{
				if (canChargedAttack)
				{
					chargedLastUsedTime = Time.time;
					// Trigger the charged attack animation
					animator.SetTrigger("ChargedAttack");

					// Move the character forward gradually
					StartCoroutine(LerpCharacterForward());

					// Reset the chargedAttack state
					starterAssetsInputs.chargedAttack = false;
				}
			}
		}
	}
	
	IEnumerator LerpCharacterForward()
	{
		Vector3 startPosition = transform.position;
		Vector3 targetPosition = transform.position + transform.forward * chargedAttackDistance;
		float duration = chargedAttackTimeNeeded; // Desired duration for the lerp (in seconds)
		float startTime = Time.time;

		while (Time.time - startTime < duration)
		{
			// Calculate the progress of the lerping based on the elapsed time and duration
			float progress = (Time.time - startTime) / duration;

			// Calculate the new position based on lerping
			Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, progress);

			// Perform collision check using Raycast
			RaycastHit hit;
			if (Physics.Raycast(newPosition, targetPosition - newPosition, out hit, Vector3.Distance(newPosition, targetPosition)))
			{
				// Collision detected, adjust target position to hit point
				targetPosition = hit.point;
			}

			// Update the character's position
			transform.position = newPosition;

			yield return null;
		}

		// Ensure that the character ends up at the correct position
		transform.position = targetPosition;

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
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			swordGameObject.SetActive(false);
			Debug.Log("Hide Sword");
		}
	}
	
	private void OnChargedAttackStart(AnimationEvent animationEvent)
	{
		meleeLayer = false;
		animator.SetLayerWeight(2, 0f);
	}
	
	private void OnChargedAttackEnd(AnimationEvent animationEvent)
	{
		meleeLayer = true;
		animator.SetLayerWeight(2, 0.8f);	
	}
	
	private void OnRangedAttackEnd(AnimationEvent animationEvent)
	{

		animator.ResetTrigger("Recoil");
		Debug.Log("Stop Furthure Shooting");
	}
	
}
