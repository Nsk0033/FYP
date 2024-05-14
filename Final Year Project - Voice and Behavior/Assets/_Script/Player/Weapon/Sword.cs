using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class Sword : MonoBehaviour
{
	[Header("Character Component")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private GameObject mainCharacter;
	
	[Header("Character Melee Attack")]
	[SerializeField] private float MeleeAttackCD = 0.5f;
	[SerializeField] private bool canMeleeAttack = true;
	
	[Header("Character Charged Attack")]
	[SerializeField] private float ChargedAttackCD = 2f;
	[SerializeField] private bool canChargedAttack = true;
	[SerializeField] private float chargedAttackTimeNeeded = 0.5f;
	[SerializeField] private float chargedAttackDistance = 3f;
	
	[Header("Character Ranged Attack")]
	//[SerializeField] public int bulletCount = 5;
    //[SerializeField] public float spreadAngle = 30f;
	[SerializeField] private float RangeAttackCD = 0.3f;
	[SerializeField] private bool canRangeAttack = true;
	[SerializeField] private float normalSensitivity = 1f;
	[SerializeField] private float aimSensitivity = 0.7f;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	[SerializeField] private Transform debugTransform;
	//[SerializeField] private Transform pfBulletProjectile;
	[SerializeField] private Transform spawnBulletPosition;
	[SerializeField] private GameObject crosshairCanva;
	
	[Header("Character Weapon")]
	[SerializeField] private GameObject swordGameObject;
	
	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
	private new BoxCollider collider;
	private float aimRigWeight;
	private float meleeLastUsedTime;
	private float rangeLastUsedTime;
	private float chargedLastUsedTime;
	private bool isIdlePlaying;
	
	private void Awake()
	{
		thirdPersonController = mainCharacter.GetComponent<ThirdPersonController>();
		starterAssetsInputs = mainCharacter.GetComponent<StarterAssetsInputs>();
		animator = mainCharacter.GetComponent<Animator>();
		collider = GetComponent<BoxCollider>();
	}
	
    // Start is called before the first frame update
    void Start()
    {
        meleeLastUsedTime = -MeleeAttackCD;
		rangeLastUsedTime = -RangeAttackCD;
		chargedLastUsedTime = -ChargedAttackCD;
		crosshairCanva.SetActive(false);
		//swordGameObject.SetActive(false);
		//animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
		//animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0.8f, Time.deltaTime * 10f));
    }
	
	

    // Update is called once per frame
    void Update()
    {
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		
		canMeleeAttack = Time.time - meleeLastUsedTime > MeleeAttackCD; //melee attack cooldown check
		canRangeAttack = Time.time - rangeLastUsedTime > RangeAttackCD; //ranged attack cooldown check
		canChargedAttack = Time.time - chargedLastUsedTime > ChargedAttackCD; //charged attack cooldown check
		
		isIdlePlaying = animator.GetCurrentAnimatorStateInfo(2).IsName("Empty");
		if (isIdlePlaying)
		{
			collider.enabled = false;
		}
		else
			collider.enabled = true;
		
		if (Physics.Raycast(ray,out RaycastHit raycastHit, 999f, aimColliderLayerMask))
		{
			debugTransform.position = raycastHit.point;
			mouseWorldPosition = raycastHit.point;
			/*if (raycastHit.collider.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
			{
				//rigidbody.AddExplosionForce(1000f,targetPosition,5f);
			}*/
		}
		else
        {
            mouseWorldPosition = ray.GetPoint(10);
            debugTransform.position = ray.GetPoint(10);
        }
	
		if (starterAssetsInputs.aim)
		{
			//play another animation <-------------------------------------------HERE
			crosshairCanva.SetActive(true);
			aimVirtualCamera.gameObject.SetActive(true);
			thirdPersonController.SetSensitivity(aimSensitivity);
			thirdPersonController.SetRotateOnMove(false);
			
			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = mainCharacter.transform.position.y;
			Vector3 aimDirection = (worldAimTarget - mainCharacter.transform.position).normalized;
			
			mainCharacter.transform.forward = Vector3.Lerp(mainCharacter.transform.forward, aimDirection, Time.deltaTime * 20f);
		}
		else
		{
			animator.ResetTrigger("MeleeAttack");
			animator.ResetTrigger("ChargedAttack");
			//swordGameObject.SetActive(true);
			crosshairCanva.SetActive(false);
			aimVirtualCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
			thirdPersonController.SetRotateOnMove(true);
			
			if(!canMeleeAttack)
			{
				//animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
				//hasMeleeAttacked = false;
				Vector3 worldAimTarget = mouseWorldPosition;
				worldAimTarget.y = mainCharacter.transform.position.y;
				Vector3 aimDirection = (worldAimTarget - mainCharacter.transform.position).normalized;
				
				mainCharacter.transform.forward = Vector3.Lerp(mainCharacter.transform.forward, aimDirection, Time.deltaTime * 20f);
			}
		}
	
		
		if (starterAssetsInputs.shoot)
		{
			if(starterAssetsInputs.aim)
			{
				if(canRangeAttack)
				{
					//if can range attack but not attacking then do this
					animator.SetTrigger("Recoil"); //<------------------------------HERE
					Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
					
					//Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
					/*for (int i = 0; i < bulletCount; i++)
					{
						float Yangle = Random.Range(-spreadAngle, spreadAngle);
						float Xangle = Random.Range((-spreadAngle*0.5f), (spreadAngle*0.5f));
						Quaternion rotation = Quaternion.Euler(Xangle, Yangle, 0f);

						Vector3 aimSpreadDir = rotation * aimDir;
						Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimSpreadDir, Vector3.up));
					}*/
					starterAssetsInputs.shoot = false;
					rangeLastUsedTime = Time.time;
					// if attacking then do ntg
					
					Invoke("StabAnimationChecking",0.3f);
				}
				else
					starterAssetsInputs.shoot = false;
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
					//StartCoroutine(LerpCharacterForward());

					// Reset the chargedAttack state
					starterAssetsInputs.chargedAttack = false;
				}
			}
		}
	}
	
	IEnumerator LerpCharacterForward()
	{
		Vector3 startPosition = mainCharacter.transform.position;
		Vector3 targetPosition = mainCharacter.transform.position + mainCharacter.transform.forward * chargedAttackDistance;
		float duration = chargedAttackTimeNeeded; // Desired duration for the lerp (in seconds)
		float startTime = Time.time;
		//Invoke("SmallJumping",0.1f);
		
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
			mainCharacter.transform.position = newPosition;

			yield return null;
		}

		// Ensure that the character ends up at the correct position
		mainCharacter.transform.position = targetPosition;
		Invoke("MCCanMove",0.2f);
		// Trigger the jump action
		//starterAssetsInputs.jump = true;
		
	}
	
	private void SmallJumping()
	{
		thirdPersonController.SmallJump();
	}
	
	private void MCCanMove()
	{
		thirdPersonController.MoveTrigger(true);
	}
	
	private void StabAnimationChecking()
	{
		bool isPlaying = animator.GetCurrentAnimatorStateInfo(2).IsName("Stab Forward");
		if (isPlaying)
		{
			thirdPersonController.MoveTrigger(false);
			thirdPersonController.SmallJump();
			StartCoroutine(LerpCharacterForward());
		}
	}

	
}
