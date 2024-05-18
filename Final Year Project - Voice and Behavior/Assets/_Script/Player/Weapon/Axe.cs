using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using StarterAssets;
using Cinemachine;
using UnityEngine.InputSystem;
using TrailsFX;

public class Axe : MonoBehaviour
{
    [Header("Public References")]
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform spine;
    [SerializeField] private Transform curvePoint;

	[Space]
	[Header("Character Component")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private GameObject mainCharacter;

    [Space]
    [Header("Parameters")]
    [SerializeField] private float throwPower = 30;

    [Space]
    [Header("Bools")]
    [SerializeField] private bool aiming = false;
    [SerializeField] private bool hasWeapon = true;
    [SerializeField] private bool pulling = false;

    [Space]
    [Header("Particles and Trails")]
    [SerializeField] private ParticleSystem glowParticle;
    [SerializeField] private ParticleSystem catchParticle;
	
	[Space]
	[Header("Character Melee Attack")]
	[SerializeField] private float MeleeAttackCD = 0.5f;
	[SerializeField] private bool canMeleeAttack = true;
	
	[Header("Character Charged Attack")]
	[SerializeField] private float ChargedAttackCD = 2f;
	[SerializeField] private bool canChargedAttack = true;
	//[SerializeField] private float chargedAttackTimeNeeded = 0.5f;
	//[SerializeField] private float chargedAttackDistance = 3f;
	
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
	//[SerializeField] private Transform spawnBulletPosition;
	[SerializeField] private GameObject crosshairCanva;

    private Rigidbody weaponRb;
    private AxeThrow axeThrow;
    private TrailEffect trailEffect;
    private float returnTime;

    private Vector3 origLocPos;
    private Vector3 origLocRot;
    private Vector3 pullPosition;

	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
	private new BoxCollider collider;
	private float aimRigWeight;
    private float rangeLastUsedTime;
	private float meleeLastUsedTime;
	private float chargedLastUsedTime;
	private bool isIdlePlaying;

	private void Awake()
	{
		thirdPersonController = mainCharacter.GetComponent<ThirdPersonController>();
		starterAssetsInputs = mainCharacter.GetComponent<StarterAssetsInputs>();
		animator = mainCharacter.GetComponent<Animator>();
        weaponRb = weapon.GetComponent<Rigidbody>();
        axeThrow = weapon.GetComponent<AxeThrow>();
        origLocPos = weapon.localPosition;
        origLocRot = weapon.localEulerAngles;
		collider = weapon.GetComponent<BoxCollider>();
		trailEffect = weapon.GetComponent<TrailEffect>();
	}

    // Start is called before the first frame update
    void Start()
    {
		meleeLastUsedTime = -MeleeAttackCD;
		rangeLastUsedTime = -RangeAttackCD;
		chargedLastUsedTime = -ChargedAttackCD;
        crosshairCanva.SetActive(false);
    }

    void Update()
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
			
			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = mainCharacter.transform.position.y;
			Vector3 aimDirection = (worldAimTarget - mainCharacter.transform.position).normalized;
			
			mainCharacter.transform.forward = Vector3.Lerp(mainCharacter.transform.forward, aimDirection, Time.deltaTime * 20f);
			if(hasWeapon)
			{
				Aim(true);
				glowParticle.Play();
			}
			
			bool isAimPlaying = animator.GetCurrentAnimatorStateInfo(3).IsName("Aim");
			if(starterAssetsInputs.shoot && isAimPlaying)
			{
				if (canRangeAttack && hasWeapon)
				{
					animator.SetTrigger("Recoil");
					WeaponThrow((mouseWorldPosition - weapon.position).normalized);
					rangeLastUsedTime = Time.time;
				}
				else
				{
					starterAssetsInputs.shoot = false;
				}
			}
			else if(starterAssetsInputs.shoot && !hasWeapon)
			{
				WeaponStartPull();
			}
			else
				starterAssetsInputs.shoot = false;
        }
        else
        {
            animator.ResetTrigger("MeleeAttack");
			animator.ResetTrigger("ChargedAttack");
			crosshairCanva.SetActive(false);
			aimVirtualCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
			thirdPersonController.SetRotateOnMove(true);
			
			if(!canMeleeAttack)
			{
				
				Vector3 worldAimTarget = mouseWorldPosition;
				worldAimTarget.y = mainCharacter.transform.position.y;
				Vector3 aimDirection = (worldAimTarget - mainCharacter.transform.position).normalized;
				
				mainCharacter.transform.forward = Vector3.Lerp(mainCharacter.transform.forward, aimDirection, Time.deltaTime * 20f);
			}
			if(hasWeapon)
			{
				Aim(false);
				glowParticle.Stop();
			}
        }
		
		if (starterAssetsInputs.shoot)
		{
			if (!starterAssetsInputs.aim)
			{
				if (canMeleeAttack && hasWeapon)
				{
					meleeLastUsedTime = Time.time;
					animator.SetTrigger("MeleeAttack");
					starterAssetsInputs.shoot = false;
					Debug.Log("Attack");
				}
				else
				{
					if(!hasWeapon)
					{
						WeaponStartPull();
					}
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
				if (canChargedAttack && hasWeapon)
				{
					chargedLastUsedTime = Time.time;
					// Trigger the charged attack animation
					animator.SetTrigger("ChargedAttack");

					// Move the character forward gradually
					//StartCoroutine(LerpCharacterForward());

					// Reset the chargedAttack state
					starterAssetsInputs.chargedAttack = false;
				}
				else if(!hasWeapon)
				{	
					WeaponStartPull();
				}
				//else if(pulling && canChargedAttack)
			}
			
			
		}
		
		isIdlePlaying = animator.GetCurrentAnimatorStateInfo(3).IsName("Empty");
		if (isIdlePlaying && hasWeapon)
		{
			collider.enabled = false;
			trailEffect.enabled = false;
		}
		else
		{
			collider.enabled = true;
			trailEffect.enabled = true;
		}
		
		bool isChargedPlaying = animator.GetCurrentAnimatorStateInfo(3).IsName("Spin");
		if (isChargedPlaying)
		{
			ResetRotation();
		}
		
		bool isPlaying = animator.GetCurrentAnimatorStateInfo(3).IsName("Pull2");
		if (isPlaying)
		{
			//WeaponCatch();
			Invoke("ResetRotation",0.35f);
			//canMeleeAttack = true;
			//MeleeAttackCD = 0.1f;
		}
        //Animation States
        animator.SetBool("Pulling", pulling);

        if (pulling)
        {
            if(returnTime < 1)
            {
                weapon.position = GetQuadraticCurvePoint(returnTime, pullPosition, curvePoint.position, hand.position);
                returnTime += Time.deltaTime * 1.5f;
            }
            else
            {
                WeaponCatch();
            }
        }
    }

    void Aim(bool state)
    {
        aiming = state;

        animator.SetBool("RangeAttacking", aiming);
    }

    public void WeaponThrow(Vector3 throwDirection)
	{
		Aim(false);
		glowParticle.Play();
		// Calculate the rotation to face the throw direction
		Quaternion targetRotation = Quaternion.LookRotation(throwDirection);

		// Gradually rotate the weapon towards the throw direction
		float rotationSpeed = 5f; // Adjust the rotation speed as needed
		weapon.rotation = Quaternion.Lerp(weapon.rotation, targetRotation, Time.deltaTime * rotationSpeed);

		// Continue with the throw logic
		hasWeapon = false;
		axeThrow.activated = true;
		weaponRb.isKinematic = false;
		weaponRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
		weapon.parent = null;
		weapon.transform.position += transform.right / 5;
		weaponRb.AddForce(throwDirection * throwPower + transform.up * 2, ForceMode.Impulse);
	}

    public void WeaponStartPull()
    {
        pullPosition = weapon.position;
        weaponRb.Sleep();
        weaponRb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        weaponRb.isKinematic = true;
        //weapon.DORotate(new Vector3(0, -90, -90), .2f).SetEase(Ease.InOutSine);
        weapon.DOBlendableLocalRotateBy(Vector3.right * 90, .5f);
        axeThrow.activated = true;
        pulling = true;
    }

    public void WeaponCatch()
    {
		returnTime = 0;
		pulling = false;
		weapon.parent = hand;
		axeThrow.activated = false;
		glowParticle.Stop();
		

		// Use DOTween to set the rotation smoothly
		weapon.DORotate(Vector3.zero, 0.15f).SetEase(Ease.InOutSine); // Use Vector3.zero to represent (0, 0, 0) rotation

		// Debug the final rotation
		Debug.Log("Final Rotation: " + weapon.rotation.eulerAngles);

		// Reset the local position
		weapon.localPosition = origLocPos;
		hasWeapon = true;
		starterAssetsInputs.shoot = false;
		// Start catch particle effect
		catchParticle.Play();
		//weaponTrailEffect.enabled = false;
		
		Invoke("ResetRotation",0.35f);
		if(weapon.localRotation != Quaternion.Euler(Vector3.zero))
		{
			ResetRotation();
		}
	}

    public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }

	private void ResetRotation()
	{
		weapon.localRotation = Quaternion.Euler(Vector3.zero);
	}
}
