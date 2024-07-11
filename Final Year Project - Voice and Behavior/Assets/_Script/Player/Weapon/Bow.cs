using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class Bow : MonoBehaviour
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
	//[SerializeField] private float chargedAttackTimeNeeded = 0.5f;
	//[SerializeField] private float chargedAttackDistance = 3f;
	
	[Header("Character Ranged Attack")]
	[SerializeField] public int bulletCount = 3;
    //[SerializeField] public float spreadAngle = 15f;
	[SerializeField] private float RangeAttackCD = 0.3f;
	[SerializeField] private bool canRangeAttack = true;
	[SerializeField] private float normalSensitivity = 1f;
	[SerializeField] private float aimSensitivity = 0.7f;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	[SerializeField] private Transform debugTransform;
	//[SerializeField] private Transform pfBulletProjectile;
	//[SerializeField] private Transform spawnBulletPosition;
	[SerializeField] private GameObject crosshairCanva;
	
	//[Header("Character Weapon")]
	//[SerializeField] private GameObject bowGameObject;
	
	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
	private PlayerActionPoint playerActionPoint;
	private PlayerLimit playerLimit;
	private float aimRigWeight;
    private float rangeLastUsedTime;
	private float meleeLastUsedTime;
	private float chargedLastUsedTime;
	private bool isUltiPlaying;
	private bool isSkill1Playing;
	private bool isSkillEPlaying;
	
    private void Awake()
	{
		thirdPersonController = mainCharacter.GetComponent<ThirdPersonController>();
		starterAssetsInputs = mainCharacter.GetComponent<StarterAssetsInputs>();
		animator = mainCharacter.GetComponent<Animator>();
		playerActionPoint = mainCharacter.GetComponent<PlayerActionPoint>();
		playerLimit = mainCharacter.GetComponent<PlayerLimit>();
	}
	
	private void Start()
	{
		meleeLastUsedTime = -MeleeAttackCD;
		rangeLastUsedTime = -RangeAttackCD;
		chargedLastUsedTime = -ChargedAttackCD;
		
		crosshairCanva.SetActive(true);
		//animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
	}
	
	private void OnEnable()
	{
		crosshairCanva.SetActive(true);
	}
	
	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		
		canMeleeAttack = Time.time - meleeLastUsedTime > MeleeAttackCD; //melee attack cooldown check
		canRangeAttack = Time.time - rangeLastUsedTime > RangeAttackCD; //ranged attack cooldown check
		canChargedAttack = Time.time - chargedLastUsedTime > ChargedAttackCD; //charged attack cooldown check
		
		isUltiPlaying = animator.GetCurrentAnimatorStateInfo(1).IsName("Ultimate");
		isSkill1Playing = animator.GetCurrentAnimatorStateInfo(1).IsName("BowSkill1");
		isSkillEPlaying = animator.GetCurrentAnimatorStateInfo(1).IsName("BowSkillE");
		
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
			animator.SetBool("RangeAttacking",true);
			//crosshairCanva.SetActive(true);
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
			animator.SetBool("RangeAttacking",false);
			animator.ResetTrigger("MeleeAttack");
			animator.ResetTrigger("ChargedAttack");
			//crosshairCanva.SetActive(true);
			aimVirtualCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
			thirdPersonController.SetRotateOnMove(true);
			
			if(!canMeleeAttack || isUltiPlaying || isSkill1Playing || isSkillEPlaying)
			{
				thirdPersonController.SetRotateOnMove(false);
				Vector3 worldAimTarget = mouseWorldPosition;
				worldAimTarget.y = mainCharacter.transform.position.y;
				Vector3 aimDirection = (worldAimTarget - mainCharacter.transform.position).normalized;
				
				mainCharacter.transform.forward = Vector3.Lerp(mainCharacter.transform.forward, aimDirection, Time.deltaTime * 60f);
			}
		}
	
		
		if (starterAssetsInputs.shoot)
		{
			if(starterAssetsInputs.aim)
			{
				if(canRangeAttack && !isUltiPlaying)
				{
					bool isPlaying = animator.GetCurrentAnimatorStateInfo(1).IsName("Bow_Idle");
					if (isPlaying)
					{
						//if can range attack but not attacking then do this
						animator.SetTrigger("Recoil");
						/*Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
						Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
						for (int i = 0; i < bulletCount; i++)
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
					}
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
				if (canChargedAttack && playerActionPoint.currentActionPointValue > 15f && !isUltiPlaying)
				{
					chargedLastUsedTime = Time.time;
					// Trigger the charged attack animation
					animator.SetTrigger("ChargedAttack");

					// Move the character forward gradually
					//StartCoroutine(LerpCharacterForward());

					// Reset the chargedAttack state
					starterAssetsInputs.chargedAttack = false;
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
		
		if(starterAssetsInputs.skill3)
		{
			if(playerActionPoint.currentActionPointAvailable > 0 && !isUltiPlaying)
			{
				animator.SetTrigger("Skill3");
				starterAssetsInputs.skill3 = false;
			}
			else
			{
				animator.ResetTrigger("Skill3");
				starterAssetsInputs.skill3 = false;
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
	}
}
