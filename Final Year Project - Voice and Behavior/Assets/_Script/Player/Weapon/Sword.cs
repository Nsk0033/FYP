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
	private PlayerActionPoint playerActionPoint;
	private PlayerLimit playerLimit;
	private new BoxCollider collider;
	private float aimRigWeight;
	private float meleeLastUsedTime;
	private float rangeLastUsedTime;
	private float chargedLastUsedTime;
	private bool isIdlePlaying;
	private bool isUltiPlaying;
	private bool isSkill2Playing;
	
	private void Awake()
	{
		thirdPersonController = mainCharacter.GetComponent<ThirdPersonController>();
		starterAssetsInputs = mainCharacter.GetComponent<StarterAssetsInputs>();
		animator = mainCharacter.GetComponent<Animator>();
		collider = GetComponent<BoxCollider>();
		playerActionPoint = mainCharacter.GetComponent<PlayerActionPoint>();
		playerLimit = mainCharacter.GetComponent<PlayerLimit>();
	}
	
    // Start is called before the first frame update
    void Start()
    {
        meleeLastUsedTime = -MeleeAttackCD;
		rangeLastUsedTime = -RangeAttackCD;
		chargedLastUsedTime = -ChargedAttackCD;
		crosshairCanva.SetActive(false);
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
		
		isUltiPlaying = animator.GetCurrentAnimatorStateInfo(2).IsName("Ultimate");
		
		isIdlePlaying = animator.GetCurrentAnimatorStateInfo(2).IsName("Empty");
		
		isSkill2Playing = animator.GetCurrentAnimatorStateInfo(2).IsName("Skill2");
		
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
			if(!isUltiPlaying)
			{	
				thirdPersonController.SetRotateOnMove(true);
			}
			
			if(!canMeleeAttack || isUltiPlaying || isSkill2Playing)
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
				if(canRangeAttack && !isUltiPlaying)
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
				if (canMeleeAttack && !isUltiPlaying)
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
				if(playerActionPoint.currentActionPointValue > 15f) 
				{
					if (canChargedAttack && !isUltiPlaying)
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
				else
					starterAssetsInputs.chargedAttack = false;
			} 
		}
		
		if(starterAssetsInputs.skillE)
		{
			if(playerActionPoint.currentActionPointAvailable > 0 && !isUltiPlaying)
			{
				animator.SetTrigger("SkillE");
				starterAssetsInputs.skillE = false;
			}
			else
			{
				animator.ResetTrigger("SkillE");
				starterAssetsInputs.skillE = false;
				return;
			}
		}
		
		if(starterAssetsInputs.skill1)
		{
			if(playerActionPoint.currentActionPointAvailable > 0 && !isUltiPlaying)
			{
				animator.SetTrigger("Skill1");
				starterAssetsInputs.skill1 = false;
			}
			else
			{
				animator.ResetTrigger("Skill1");
				starterAssetsInputs.skill1 = false;
				return;
			}
		}
		
		if(starterAssetsInputs.skillQ)
		{
			if(playerLimit.currentLimit == playerLimit.maxLimit && !isUltiPlaying)
			{
				animator.SetTrigger("SkillQ");
				starterAssetsInputs.skillQ = false;
			}
			else
			{
				animator.ResetTrigger("SkillQ");
				starterAssetsInputs.skillQ = false;
				return;
			}
		}
		
		if(starterAssetsInputs.skill2)
		{
			if(playerActionPoint.currentActionPointAvailable > 0 && !isUltiPlaying)
			{
				animator.SetTrigger("Skill2");
				starterAssetsInputs.skill2 = false;
			}
			else
			{
				animator.ResetTrigger("Skill2");
				starterAssetsInputs.skill2 = false;
				return;
			}
		}
	}
	
	IEnumerator LerpCharacterForward(Vector3 targetPoint)
    {
        Vector3 startPosition = mainCharacter.transform.position;
        Vector3 direction = (targetPoint - startPosition).normalized;
        Vector3 targetPosition = startPosition + direction * chargedAttackDistance;
        float duration = chargedAttackTimeNeeded;
        float startTime = Time.time;

        CharacterController characterController = mainCharacter.GetComponent<CharacterController>();

        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime) / duration;
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, progress);
            newPosition = AdjustPositionToGround(newPosition, characterController);

            if (Physics.Raycast(newPosition, targetPosition - newPosition, out RaycastHit forwardHit, Vector3.Distance(newPosition, targetPosition)))
            {
                targetPosition = forwardHit.point;
            }

            characterController.Move(newPosition - mainCharacter.transform.position);

            yield return null;
        }

        Vector3 finalPosition = AdjustPositionToGround(targetPosition, characterController);
        characterController.Move(finalPosition - mainCharacter.transform.position);

        Invoke("MCCanMove", 0.2f);
    }

    private Vector3 AdjustPositionToGround(Vector3 position, CharacterController characterController)
    {
        RaycastHit hit;
        float raycastHeightOffset = 0.5f;
        float raycastDistance = 1.0f;
        Vector3 rayOrigin = position + Vector3.up * raycastHeightOffset;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance))
        {
            position.y = hit.point.y + characterController.skinWidth;
        }

        return position;
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
			StartCoroutine(LerpCharacterForward(debugTransform.position));
		}
	}

	
}
