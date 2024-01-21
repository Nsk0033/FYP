using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
	[SerializeField] private Rig aimRig;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
	[SerializeField] private float normalSensitivity;
	[SerializeField] private float aimSensitivity;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	[SerializeField] private Transform debugTransform;
	[SerializeField] private Transform pfBulletProjectile;
	[SerializeField] private Transform spawnBulletPosition;
	[SerializeField] private GameObject crosshairCanva;
	[SerializeField] private GameObject MeleeClone1;
	[SerializeField] private GameObject MeleeClone2;
	[SerializeField] private GameObject MeleeClone3;
	[SerializeField] private GameObject MeleeClone4;
	[SerializeField] private int meleeCounter = 1;
	[SerializeField] private float MeleeAttackCD = 0.5f;
	[SerializeField] private bool canMeleeAttack = true;
	[SerializeField] private bool hasMeleeAttacked;
	[SerializeField] public int bulletCount = 5;
    [SerializeField] public float spreadAngle = 30f;
	[SerializeField] private float RangeAttackCD = 0.3f;
	[SerializeField] private bool canRangeAttack = true;
	
	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
	private Animator cloneAnimator1;
	private Animator cloneAnimator2;
	private Animator cloneAnimator3;
	private Animator cloneAnimator4;
	private float aimRigWeight;
    private float rangeLastUsedTime;
	private float meleeLastUsedTime;
	
	
	private void Awake()
	{
		
		thirdPersonController = GetComponent<ThirdPersonController>();
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		animator = GetComponent<Animator>();
		GameObject nyxClone1 = GameObject.Find("NyxClone1");
        if (nyxClone1 != null)
		{
			cloneAnimator1 = nyxClone1.GetComponent<Animator>();
			MeleeClone1.SetActive(false);
		}
		else
		{
			Debug.Log("Cant find clone 1");
		}
		GameObject nyxClone2 = GameObject.Find("NyxClone2");
        if (nyxClone2 != null)
		{
			cloneAnimator2 = nyxClone2.GetComponent<Animator>();
			MeleeClone2.SetActive(false);
		}
		else
		{
			Debug.Log("Cant find clone 2");
		}
		GameObject nyxClone3 = GameObject.Find("NyxClone3");
		if (nyxClone3 != null)
		{
			cloneAnimator3 = nyxClone3.GetComponent<Animator>();
			MeleeClone3.SetActive(false);
		}
		else
		{
			Debug.Log("Cant find clone 3");
		}
		GameObject nyxClone4 = GameObject.Find("NyxClone4");
		if (nyxClone4 != null)
		{
			cloneAnimator4 = nyxClone4.GetComponent<Animator>();
			MeleeClone4.SetActive(false);
		}
		else
		{
			Debug.Log("Cant find clone 4");
		}
	}
	
	private void Start()
	{
		meleeLastUsedTime = -MeleeAttackCD;
		rangeLastUsedTime = -RangeAttackCD;
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
			crosshairCanva.SetActive(true);
			aimVirtualCamera.gameObject.SetActive(true);
			thirdPersonController.SetSensitivity(aimSensitivity);
			thirdPersonController.SetRotateOnMove(false);
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
			
			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
			
			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
			
			aimRig.weight = 1f;
		}
		else
		{
			crosshairCanva.SetActive(false);
			aimVirtualCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
			thirdPersonController.SetRotateOnMove(true);
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
			aimRig.weight = 0f;
			
			if(canMeleeAttack)
			{
				animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
				hasMeleeAttacked = false;
				
			}
			else
			{
				if(!hasMeleeAttacked)
				{
					thirdPersonController.SetRotateOnMove(true);
					animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 200f));
					
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
					if(meleeCounter == 1)
					{				
						meleeLastUsedTime = Time.time;
						MeleeClone1.SetActive(true);
						cloneAnimator1.SetTrigger("isMelee");
						starterAssetsInputs.shoot = false;
						Invoke("HideClone1",0.7f);
						meleeCounter++;
					}
					else if(meleeCounter == 2)
					{		
						meleeLastUsedTime = Time.time;
						MeleeClone2.SetActive(true);
						cloneAnimator2.SetTrigger("isMelee");
						starterAssetsInputs.shoot = false;
						Invoke("HideClone2",0.7f);
						meleeCounter++;
					}
					else if(meleeCounter == 3)
					{	
						meleeLastUsedTime = Time.time;
						MeleeClone3.SetActive(true);
						cloneAnimator3.SetTrigger("isMelee");
						starterAssetsInputs.shoot = false;
						Invoke("HideClone3",1.5f);
						meleeCounter++;
					}
					else if(meleeCounter == 4)
					{	
						meleeLastUsedTime = Time.time;
						MeleeClone4.SetActive(true);
						cloneAnimator4.SetTrigger("isMelee");
						starterAssetsInputs.shoot = false;
						Invoke("HideClone4",2f);
						meleeCounter = 1;
					}
				}
			}
		}	
	}
	
	private void HideClone1()
	{
		MeleeClone1.SetActive(false);
	}
	
	private void HideClone2()
	{
		MeleeClone2.SetActive(false);
	}
	
	private void HideClone3()
	{
		MeleeClone3.SetActive(false);
	}
	
	private void HideClone4()
	{
		MeleeClone4.SetActive(false);
	}
}
