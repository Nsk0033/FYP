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
	[SerializeField] private float MeleeAttackCD = 0.5f;
	[SerializeField] private bool canMeleeAttack = true;
	[SerializeField] private bool hasMeleeAttacked;
	[SerializeField] public int bulletCount = 5;
    [SerializeField] public float spreadAngle = 30f;
	[SerializeField] private float RangeAttackCD = 0.3f;
	[SerializeField] private bool canRangeAttack = true;
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
			animator.ResetTrigger("MeleeAttack");
			//swordGameObject.SetActive(true);
			bowGameObject.SetActive(false);
			crosshairCanva.SetActive(false);
			aimVirtualCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
			thirdPersonController.SetRotateOnMove(true);
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
			//aimRig.weight = 0f;
			
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
					/*
					if(meleeCounter == 1)
					{				
						meleeLastUsedTime = Time.time;
						//swordGameObject.SetActive(true);
						animator.SetTrigger("MeleeAttack");
						
						starterAssetsInputs.shoot = false;
						meleeCounter++;
					}
					else if(meleeCounter == 2)
					{		
						meleeLastUsedTime = Time.time;
						//swordGameObject.SetActive(true);
						animator.SetTrigger("MeleeAttack");
						
						starterAssetsInputs.shoot = false;
						
						meleeCounter--;
					}
					/*else if(meleeCounter == 3)
					{	
						meleeLastUsedTime = Time.time;
						//swordGameObject.SetActive(true);
						animator.SetTrigger("MeleeAttack");
						starterAssetsInputs.shoot = false;
						
						meleeCounter++;
					}
					else if(meleeCounter == 4)
					{	
						meleeLastUsedTime = Time.time;
						//swordGameObject.SetActive(true);
						animator.SetTrigger("MeleeAttack");
						starterAssetsInputs.shoot = false;
					
						meleeCounter = 1;
					} */
					Debug.Log("Attack");
				}
			}
		}	
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
}
