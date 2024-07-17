using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using FirstGearGames.SmoothCameraShaker;

public class PlayerAnimationEventTrigger : MonoBehaviour
{
	[Header("Character Components")]
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Transform mainCharacter;
	[SerializeField] private GameObject ultiCinemachine;
	[SerializeField] private ShakeData shakeData;  // Assign the ShakeData in the inspector
	[SerializeField] private Transform mainCharacterBow;
	
	[Header("Character Value")]
	[SerializeField] private float animatorHitStopSpeed = 0.3f;
	
	[Header("Character Sword Projectile")]
	[SerializeField] private GameObject s_attack4Slash;
	[SerializeField] private GameObject s_ChargedSlash1;
	[SerializeField] private GameObject s_ChargedSlash2;
	[SerializeField] private GameObject s_RangedSlash1;
	[SerializeField] private GameObject s_LimitSlash;
	[SerializeField] private GameObject s_LimitSlash1;
	[SerializeField] private GameObject s_LimitAura;
	[SerializeField] private GameObject s_LimitAura1;
	[SerializeField] private GameObject s_SkillEAura;
	[SerializeField] private GameObject s_Skill1Object;
	[SerializeField] private GameObject s_Skill1FlameSlash;
	[SerializeField] private GameObject s_Skill2;
	[SerializeField] private GameObject s_Skill3Shield;
	
	[Header("Character Bow Projectile")]
	[SerializeField] private float arrowSpread = 3f;
	[SerializeField] private int chargedAttackArrowCount = 3;
	[SerializeField] private GameObject b_attackProjectile;
	[SerializeField] private GameObject b_ChargedProjectile;
	[SerializeField] private GameObject b_Skill3Projectile;
	[SerializeField] private GameObject b_ImpactParticle;
	[SerializeField] private GameObject b_LimitCast; 
	[SerializeField] private GameObject b_LimitRain;
	[SerializeField] private GameObject b_LimitAulora;
	[SerializeField] private GameObject b_skill1;
	[SerializeField] private GameObject b_skille;
	[SerializeField] private GameObject b_skill2_cast;
	[SerializeField] private GameObject b_skill2_rain;
	[SerializeField] private GameObject b_skill3Object;
	[SerializeField] private GameObject b_skill3Cast;
	
	[Header("Character Axe Projectile")]
	[SerializeField] private GameObject a_LimitSpin;
	[SerializeField] private GameObject a_LimitSpin1;
	[SerializeField] private GameObject a_skille;
	[SerializeField] private GameObject a_skill1;
	[SerializeField] private GameObject a_skill2Object;
	[SerializeField] private GameObject a_skill2IceSlash;
	[SerializeField] private GameObject a_skill3Shockwave;
	
	[Header("Character Position")]
	[SerializeField] private Transform shootPosition;
	[SerializeField] private Transform shootPosition1;
	[SerializeField] private Transform shootPosition_upAir;
	[SerializeField] private Transform shootPosition_bowImpact;
	[SerializeField] private Transform shootPosition_sword4;
	[SerializeField] private Transform shootPosition_bow;
	
	private ThirdPersonShooterController thirdPersonShooterController;
	private Animator animator;
	private StarterAssetsInputs starterAssetsInputs;
	private PlayerActionPoint playerActionPoint;
	private PlayerLimit playerLimit;
	private ThirdPersonController thirdPersonController;
	
	private float lastBowCastRotationY;
	private GameObject skill2CastInstance;
	
	private void Start()
	{
		thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
		animator = mainCharacter.GetComponent<Animator>();
		starterAssetsInputs = mainCharacter.GetComponent<StarterAssetsInputs>();
		playerActionPoint = mainCharacter.GetComponent<PlayerActionPoint>();
		playerLimit = mainCharacter.GetComponent<PlayerLimit>();
		thirdPersonController = GetComponent<ThirdPersonController>();
		if (PlayerHealth.instance == null)
		{
			Debug.LogError("PlayerHealth singleton instance is not found!");
		}
	}
	
	/*private void Update()
	{
		/*if (Input.GetKey(KeyCode.K))
		{
			AnimationHitStop();
		}
	}*/
	public void CameraStartShaking()
    {
        CameraShakerHandler.ShakeAll(shakeData);
    }

    public void CameraStopShaking()
    {
        CameraShakerHandler.StopAll();
    }
	
	public void CameraStartShakingDelay(AnimationEvent animationEvent)
    {
		float delayTime = animationEvent.floatParameter; // Get the integer parameter from the animation event
        CameraShakerHandler.ShakeAll(shakeData);
		Invoke("CameraStopShaking",delayTime);
    }
	
    private void Attack4SlashStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			// Ensure the main camera is assigned
			if (mainCamera == null)
			{
				Debug.LogWarning("Main camera is not assigned.");
				return;
			}

			// Perform a raycast from the camera to the mouse position
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits something in the scene
			if (Physics.Raycast(ray, out hit))
			{
				// Calculate the direction vector from the shootPosition to the hit point
				Vector3 direction = hit.point - mainCharacter.position;
				direction.y = 0f; // Ensure the slash stays parallel to the ground

				// Calculate the rotation based on the direction vector
				Quaternion slashRotation = Quaternion.LookRotation(direction);

				// Instantiate the attack4Slash GameObject with the calculated rotation
				Instantiate(s_attack4Slash, shootPosition_sword4.position, slashRotation);
			}
		}
        else
			return;
    }
	
	private void ChargedAttack1SlashStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			// Get the y-axis rotation of the main character
			float characterRotationY = mainCharacter.rotation.eulerAngles.y;

			// Create a new rotation based on the main character's y-axis rotation
			Quaternion slashRotation = Quaternion.Euler(0f, characterRotationY, 0f);

			// Instantiate the ChargedSlash GameObject with the calculated rotation
			Instantiate(s_ChargedSlash1, shootPosition.position, slashRotation);
		}
		else
			return;
	}
	
	private void ChargedAttack2SlashStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			// Get the y-axis rotation of the main character
			float characterRotationY = mainCharacter.rotation.eulerAngles.y;

			// Create a new rotation based on the main character's y-axis rotation
			Quaternion slashRotation = Quaternion.Euler(0f, characterRotationY, 0f);

			// Instantiate the ChargedSlash GameObject with the calculated rotation
			Instantiate(s_ChargedSlash2, shootPosition.position, slashRotation);
		}
		else
			return;
	}
	
	private void RangedAttack1SlashStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			Vector3 mouseWorldPosition = Vector3.zero;
			Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

			if (Physics.Raycast(ray,out RaycastHit raycastHit, 999f))
			{
				mouseWorldPosition = raycastHit.point;
			}
			else
			{
				mouseWorldPosition = ray.GetPoint(10);
			}
			Vector3 aimDir = (mouseWorldPosition - mainCharacter.position).normalized;
			
			Instantiate(s_RangedSlash1, shootPosition_sword4.position, Quaternion.LookRotation(aimDir, Vector3.up));
		}
		else
			return;
	}
	
	private void MeleeAttack1BowStart(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			Vector3 mouseWorldPosition = Vector3.zero;
			Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

			if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
			{
				mouseWorldPosition = raycastHit.point;
			}
			else
			{
				mouseWorldPosition = ray.GetPoint(10);
			}

			Vector3 aimDir = (mouseWorldPosition - shootPosition.position).normalized;

			// Convert aim direction to local space
			Vector3 localAimDir = mainCharacter.InverseTransformDirection(aimDir);

			// Calculate aim directions for the +3 and -3 degree offsets from the central aim direction
			Quaternion rotationPositive3 = Quaternion.Euler(arrowSpread, 0f, 0f);
			Quaternion rotationNegative3 = Quaternion.Euler(-arrowSpread, 0f, 0f);
			Quaternion rotationPositive6 = Quaternion.Euler(arrowSpread*2f, 0f, 0f);
			Quaternion rotationNegative6 = Quaternion.Euler(-arrowSpread*2f, 0f, 0f);

			Vector3 localAimDirPositive3 = rotationPositive3 * localAimDir;
			Vector3 localAimDirNegative3 = rotationNegative3 * localAimDir;
			Vector3 localAimDirPositive6 = rotationPositive6 * localAimDir;
			Vector3 localAimDirNegative6 = rotationNegative6 * localAimDir;

			// Convert local directions back to world space
			Vector3 worldAimDirPositive3 = mainCharacter.TransformDirection(localAimDirPositive3);
			Vector3 worldAimDirNegative3 = mainCharacter.TransformDirection(localAimDirNegative3);
			Vector3 worldAimDirPositive6 = mainCharacter.TransformDirection(localAimDirPositive6);
			Vector3 worldAimDirNegative6 = mainCharacter.TransformDirection(localAimDirNegative6);
			
			if(b_skill3Object.activeSelf)
			{	// Instantiate projectiles at shootPosition with the calculated aim directions
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(worldAimDirPositive3, Vector3.up));
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(worldAimDirNegative3, Vector3.up));
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(worldAimDirPositive6, Vector3.up));
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(worldAimDirNegative6, Vector3.up));
			}
			else
			{
				Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
				Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(worldAimDirPositive3, Vector3.up));
				Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(worldAimDirNegative3, Vector3.up));
			}	
		}
		else
		{
			return;
		}
	}
	
	private void MeleeAttack2BowStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			Vector3 mouseWorldPosition = Vector3.zero;
			Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

			if (Physics.Raycast(ray,out RaycastHit raycastHit, 999f))
			{
				mouseWorldPosition = raycastHit.point;
			}
			else
			{
				mouseWorldPosition = ray.GetPoint(10);
			}
			Vector3 aimDir = (mouseWorldPosition - shootPosition.position).normalized;
			

			// Calculate aim directions for the +3 and -3 degree offsets from the central aim direction
			Quaternion rotationPositive3 = Quaternion.Euler(0f, arrowSpread, 0f);
			Quaternion rotationNegative3 = Quaternion.Euler(0f, -arrowSpread, 0f);
			Quaternion rotationPositive6 = Quaternion.Euler(0f, arrowSpread*2f, 0f);
			Quaternion rotationNegative6 = Quaternion.Euler(0f, -arrowSpread*2f, 0f);

			Vector3 aimDirPositive3 = rotationPositive3 * aimDir;
			Vector3 aimDirNegative3 = rotationNegative3 * aimDir;
			Vector3 aimDirPositive6 = rotationPositive6 * aimDir;
			Vector3 aimDirNegative6 = rotationNegative6 * aimDir;
			
			if(b_skill3Object.activeSelf)
			{
				// Instantiate projectiles at shootPosition with the calculated aim directions
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(aimDirPositive3, Vector3.up));
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(aimDirNegative3, Vector3.up));
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(aimDirPositive6, Vector3.up));
				Instantiate(b_Skill3Projectile, shootPosition.position, Quaternion.LookRotation(aimDirNegative6, Vector3.up));
			}
			else
			{
				Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
				Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDirPositive3, Vector3.up));
				Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDirNegative3, Vector3.up));
			}
		}
		else
			return;
	}
	
	private void RangedAttackBowStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			Vector3 mouseWorldPosition = Vector3.zero;
			Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

			if (Physics.Raycast(ray,out RaycastHit raycastHit, 999f))
			{
				mouseWorldPosition = raycastHit.point;
			}
			else
			{
				mouseWorldPosition = ray.GetPoint(10);
			}
			Vector3 aimDir = (mouseWorldPosition - shootPosition_bow.position).normalized;
			if(b_skill3Object.activeSelf)
			{
				Instantiate(b_Skill3Projectile, shootPosition_bow.position, Quaternion.LookRotation(aimDir, Vector3.up));
				for (int i = 0; i < 5; i++)
				{
					float Yangle = Random.Range(-1f, 1);
					float Xangle = Random.Range((-1f*0.7f), (1f*0.7f));
					Quaternion rotation = Quaternion.Euler(Xangle, Yangle, 0f);

					Vector3 aimSpreadDir = rotation * aimDir;
					Instantiate(b_Skill3Projectile, shootPosition_bow.position, Quaternion.LookRotation(aimSpreadDir, Vector3.up));
				}
			}
			else
			{
				Instantiate(b_attackProjectile, shootPosition_bow.position, Quaternion.LookRotation(aimDir, Vector3.up));
				for (int i = 0; i < 3; i++)
				{
					float Yangle = Random.Range(-1f, 1f);
					float Xangle = Random.Range((-1f*0.7f), (1f*0.7f));
					Quaternion rotation = Quaternion.Euler(Xangle, Yangle, 0f);

					Vector3 aimSpreadDir = rotation * aimDir;
					Instantiate(b_attackProjectile, shootPosition_bow.position, Quaternion.LookRotation(aimSpreadDir, Vector3.up));
				}
			}	
			
		}
		else
			return;
	}

	private void ChargedAttack1BowStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			// Get the forward direction of shootPosition_upAir
			Vector3 forwardDirection = shootPosition_upAir.forward;

			// Get the current rotation of shootPosition_upAir
			Quaternion currentRotation = shootPosition_upAir.rotation;

			// Use a for loop to instantiate 3 b_ChargedProjectile objects
			for (int i = 0; i < chargedAttackArrowCount; i++)
			{
				float randomYRotation = Random.Range(-45f, 45f);
				float randomXRotation = Random.Range(-45f, 45f);

				// Create a Quaternion for the rotation with random y and z rotations
				Quaternion additionalRotation = Quaternion.Euler(randomXRotation, randomYRotation, 0f);

				// Combine the additional rotation with the current rotation
				Quaternion finalRotation = currentRotation * additionalRotation;
				// Instantiate the b_ChargedProjectile with the final rotation
				if(b_skill3Object.activeSelf) Instantiate(b_Skill3Projectile, shootPosition_upAir.position, finalRotation);
				else Instantiate(b_ChargedProjectile, shootPosition_upAir.position, finalRotation);
				
			}
		}
	}

	private void ResetBowRecoil(AnimationEvent animationEvent)
	{
		animator.ResetTrigger("Recoil");
	}
	
	private void ResetAxeThrow(AnimationEvent animationEvent)
	{
		starterAssetsInputs.shoot = false;
	}
	
	private void BowImpact(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			// Get the y-axis rotation of the main character
			float characterRotationY = mainCharacter.rotation.eulerAngles.y;

			// Create a new rotation based on the main character's y-axis rotation
			Quaternion slashRotation = Quaternion.Euler(0f, characterRotationY, 0f);

			// Instantiate the ChargedSlash GameObject with the calculated rotation
			Instantiate(b_ImpactParticle, shootPosition_bowImpact.position, slashRotation);
		}
	}
	
	public void AnimationHitStop()
	{
		animator.speed = animatorHitStopSpeed;
		Invoke("AnimationHitContinue",0.2f);
	}
	
	private void AnimationHitContinue()
	{
		animator.speed = 1f;
	}
	
	private void ReduceAP(AnimationEvent animationEvent)
	{
		int pointsToDeduct = animationEvent.intParameter; // Get the integer parameter from the animation event
		playerActionPoint.ReduceAP(pointsToDeduct);
	}
	
	private void UseAP(AnimationEvent animationEvent)
	{
		int pointsToDeduct = animationEvent.intParameter; // Get the integer parameter from the animation event
		playerActionPoint.currentActionPointAvailable -= pointsToDeduct;
		//Debug.Log(pointsToDeduct + " Action Points used. Remaining: " + playerActionPoint.currentActionPointAvailable);
	}
	
	private void UseLimit(AnimationEvent animationEvent)
	{
		playerLimit.currentLimit = 0;
	}
	
	private void UltiCamOn(AnimationEvent animationEvent)
	{
		ultiCinemachine.SetActive(true);
		animator.speed = 0.7f;
	}
	
	private void UltiCamOff(AnimationEvent animationEvent)
	{
		ultiCinemachine.SetActive(false);
		animator.speed = 1f;
	}
	
	private void CanDamageFalse()
	{
		if (PlayerHealth.instance != null)
		{
			PlayerHealth.instance.canDamage = false;
			Debug.Log("Player cant be damage");
		}
		else
		{
			Debug.LogError("PlayerHealth singleton instance is null in DamagePlayerCustom.");
		}
	}
	
	private void CanDamageTrue()
	{
		if (PlayerHealth.instance != null)
		{
			PlayerHealth.instance.canDamage = true;
			Debug.Log("Player can be damage");
		}
		else
		{
			Debug.LogError("PlayerHealth singleton instance is null in DamagePlayerCustom.");
		}
	}
	
	private void LimitAttackSlashStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			// Ensure the main camera is assigned
			if (mainCamera == null)
			{
				Debug.LogWarning("Main camera is not assigned.");
				return;
			}

			// Perform a raycast from the camera to the mouse position
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits something in the scene
			if (Physics.Raycast(ray, out hit))
			{
				// Calculate the direction vector from the shootPosition to the hit point
				Vector3 direction = hit.point - mainCharacter.position;
				direction.y = 0f; // Ensure the slash stays parallel to the ground

				// Calculate the rotation based on the direction vector
				Quaternion slashRotation = Quaternion.LookRotation(direction);

				// Instantiate the attack4Slash GameObject with the calculated rotation
				Instantiate(s_LimitSlash, shootPosition_sword4.position, slashRotation);
				Instantiate(s_LimitSlash1, shootPosition_sword4.position, slashRotation);
			}
		}
        else
			return;
    }
	
	private void LimitAttackAuraStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			GameObject ultiAura = Instantiate(s_LimitAura);
			ultiAura.transform.SetParent(mainCharacter);
			ultiAura.transform.localPosition = Vector3.zero;
			ultiAura.transform.localRotation = Quaternion.identity;
			
			GameObject ultiAura1 = Instantiate(s_LimitAura1);
			ultiAura1.transform.SetParent(mainCharacter);
			ultiAura1.transform.localPosition = Vector3.zero;
			ultiAura1.transform.localRotation = Quaternion.identity;
			//ultiAura.transform.localScale = Vector3.one;
		}
	}
	
	private void StopMovement(AnimationEvent animationEvent)
	{
		thirdPersonController.MoveTrigger(false);
	}
	
	private void StartMovement(AnimationEvent animationEvent)
	{
		thirdPersonController.MoveTrigger(true);
	}
	
	private void LimitAttackAxeStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 3)
		{
			GameObject ultiSpin = Instantiate(a_LimitSpin);
			ultiSpin.transform.SetParent(mainCharacter);
			ultiSpin.transform.localPosition = Vector3.zero;
			ultiSpin.transform.localRotation = Quaternion.identity;
			//ultiSpin.transform.localScale = Vector3.one;
		}
	}
	
	private void LimitAttackAxeEnd(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 3)
		{
			// Instantiate the a_LimitSpin1 GameObject at the player's position
			GameObject limitSpin1 = Instantiate(a_LimitSpin1, mainCharacter.position, Quaternion.identity);

			// Start a coroutine to follow the player for 2 seconds and then freeze
			StartCoroutine(FollowPlayerThenFreeze(limitSpin1, 2.5f));
		}
	}
	
	private IEnumerator FollowPlayerThenFreeze(GameObject objectToFollow, float followDuration)
	{
		float elapsedTime = 0.0f;

		while (elapsedTime < followDuration)
		{
			// Update the object's position to follow the player smoothly
			objectToFollow.transform.position = Vector3.Lerp(objectToFollow.transform.position, mainCharacter.transform.position, Time.deltaTime * 5.0f);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// After 2 seconds, freeze the object's position
		objectToFollow.transform.parent = null; // Detach from player
	}
	
	private void SkillEAxe(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 3)
		{
			// Ensure the main camera is assigned
			if (mainCamera == null)
			{
				Debug.LogWarning("Main camera is not assigned.");
				return;
			}

			// Perform a raycast from the camera to the mouse position
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits something in the scene
			if (Physics.Raycast(ray, out hit))
			{
				// Calculate the direction vector from the shootPosition to the hit point
				Vector3 direction = hit.point - mainCharacter.position;
				direction.y = 0f; // Ensure the slash stays parallel to the ground

				// Calculate the rotation based on the direction vector
				Quaternion slashRotation = Quaternion.LookRotation(direction);

				// Instantiate the attack4Slash GameObject with the calculated rotation
				Instantiate(a_skille, mainCharacter.position, slashRotation);
			}
		}
        else
			return;
	}
	
	private void LimitAttackBowStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			// Ensure the main camera is assigned
			if (mainCamera == null)
			{
				Debug.LogWarning("Main camera is not assigned.");
				return;
			}

			// Perform a raycast from the camera to the mouse position
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits something in the scene
			if (Physics.Raycast(ray, out hit))
			{
				Vector3 direction = hit.point - mainCharacter.position;
                direction.y = 0f; // Ensure the slash stays parallel to the ground

                // Calculate the rotation based on the direction vector
                Quaternion slashRotation = Quaternion.LookRotation(direction);

                // Instantiate the b_LimitCast GameObject with the calculated rotation
                GameObject castAimDirection = Instantiate(b_LimitCast, mainCharacter.position, slashRotation);

                // Store the Y-axis rotation for later use
                lastBowCastRotationY = castAimDirection.transform.rotation.eulerAngles.y;

                // Invoke SpawnLimitRainAfterDelay with the rotation value after 2 seconds
                Invoke(nameof(StartSpawnLimitRainCoroutine), 2.5f);
			}
		}
        else
			return;
    }
	
	private void StartSpawnLimitRainCoroutine()
	{
		StartCoroutine(SpawnLimitRainWithInterval());
	}
	
	private IEnumerator SpawnLimitRainWithInterval()
    {
        for (int i = 0; i < 3; i++)
        {
            // Create a new rotation with the stored Y-axis value
            Quaternion rotationWithStoredY = Quaternion.Euler(0, lastBowCastRotationY, 0);

            // Spawn b_LimitRain at the main character's position with the stored rotation
            Instantiate(b_LimitRain, mainCharacter.position, rotationWithStoredY);

            // Wait for 1 second before the next spawn
            yield return new WaitForSeconds(1f);
        }
    }
	
	private void LimitAttackAuloraStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			// Ensure the main camera is assigned
			if (mainCamera == null)
			{
				Debug.LogWarning("Main camera is not assigned.");
				return;
			}

			// Perform a raycast from the camera to the mouse position
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits something in the scene
			if (Physics.Raycast(ray, out hit))
			{
				// Calculate the direction vector from the shootPosition to the hit point
				Vector3 direction = hit.point - mainCharacter.position;
				direction.y = 0f; // Ensure the slash stays parallel to the ground

				// Calculate the rotation based on the direction vector
				Quaternion slashRotation = Quaternion.LookRotation(direction);

				// Instantiate the attack4Slash GameObject with the calculated rotation
				Instantiate(b_LimitAulora, mainCharacter.position, slashRotation);
			}
		}
        else
			return;
    }
	
	private void SkillESwordStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			GameObject ultiSpin = Instantiate(s_SkillEAura);
			ultiSpin.transform.SetParent(mainCharacter);
			ultiSpin.transform.localPosition = Vector3.zero;
			ultiSpin.transform.localRotation = Quaternion.identity;
			if (PlayerHealth.instance != null)
			{
				// Call HealPlayer function with a specified healing amount
				PlayerHealth.instance.HealPlayer(80);
			}
			else
			{
				Debug.LogWarning("PlayerHealth instance not found!");
			}
		}
	}
	
	private void Skill1SwordStart(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			s_Skill1Object.SetActive(true);
			CancelInvoke("Skill1SwordEnd");
			Invoke("Skill1SwordEnd", 15f);
		}
	}

	private void Skill1SwordEnd()
	{    
		s_Skill1Object.SetActive(false);
	}
	
	private void FlameSlashStart(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 1 && s_Skill1Object.activeSelf)
		{
			// Ensure the main camera is assigned
			if (mainCamera == null)
			{
				Debug.LogWarning("Main camera is not assigned.");
				return;
			}

			float slashZRotation = animationEvent.floatParameter;

			// Perform a raycast from the camera to the mouse position
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits something in the scene
			if (Physics.Raycast(ray, out hit))
			{
				// Calculate the direction vector from the shootPosition to the hit point
				Vector3 direction = hit.point - mainCharacter.position;
				direction.y = 0f; // Ensure the slash stays parallel to the ground

				// Calculate the rotation based on the direction vector
				Quaternion slashRotation = Quaternion.LookRotation(direction);

				// Incorporate the Z-axis rotation
				slashRotation *= Quaternion.Euler(0, 0, slashZRotation);

				// Instantiate the attack4Slash GameObject with the calculated rotation
				Instantiate(s_Skill1FlameSlash, shootPosition_sword4.position, slashRotation);
			}
		}
		else
			return;
	}
	
	private void IceSlashStart(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 3 && a_skill2Object.activeSelf)
		{
			// Ensure the main camera is assigned
			if (mainCamera == null)
			{
				Debug.LogWarning("Main camera is not assigned.");
				return;
			}

			float slashZRotation = animationEvent.floatParameter;

			// Perform a raycast from the camera to the mouse position
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits something in the scene
			if (Physics.Raycast(ray, out hit))
			{
				// Calculate the direction vector from the shootPosition to the hit point
				Vector3 direction = hit.point - mainCharacter.position;
				direction.y = 0f; // Ensure the slash stays parallel to the ground

				// Calculate the rotation based on the direction vector
				Quaternion slashRotation = Quaternion.LookRotation(direction);

				// Incorporate the Z-axis rotation
				slashRotation *= Quaternion.Euler(0, 0, slashZRotation);

				// Instantiate the attack4Slash GameObject with the calculated rotation
				Instantiate(a_skill2IceSlash, shootPosition_sword4.position, slashRotation);
			}
		}
		else
			return;
	}
	
	private void FlameRingStart(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 3)
		{
			StartCoroutine(SpawnSkill1Repeatedly());
		}
	}

	private IEnumerator SpawnSkill1Repeatedly()
	{
		int spawnCount = 0;
		while (spawnCount < 5)
		{
			Instantiate(a_skill1, mainCharacter.position, Quaternion.identity);
			spawnCount++;
			yield return new WaitForSeconds(2f);
		}
	}
	
	private void Skill1BowStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			Vector3 mouseWorldPosition = Vector3.zero;
			Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

			if (Physics.Raycast(ray,out RaycastHit raycastHit, 999f))
			{
				mouseWorldPosition = raycastHit.point;
			}
			else
			{
				mouseWorldPosition = ray.GetPoint(10);
			}
			Vector3 aimDir = (mouseWorldPosition - mainCharacter.position).normalized;

			// Instantiate projectiles at shootPosition with the calculated aim directions
			Instantiate(b_skill1, shootPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
		}
		else
			return;
	}
	
	private void SkillEBowStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			Vector3 mouseWorldPosition = Vector3.zero;
			Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

			if (Physics.Raycast(ray,out RaycastHit raycastHit, 999f))
			{
				mouseWorldPosition = raycastHit.point;
			}
			else
			{
				mouseWorldPosition = ray.GetPoint(10);
			}
			Vector3 aimDir = (mouseWorldPosition - mainCharacter.position).normalized;

			// Instantiate projectiles at shootPosition with the calculated aim directions
			Instantiate(b_skille, shootPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
		}
		else
			return;
	}
	
	private void Skill2SwordStart(AnimationEvent animationEvent)
	{
		if(thirdPersonShooterController.CurrentWeaponIndex == 1)
		{
			// Ensure the main camera is assigned
			if (mainCamera == null)
			{
				Debug.LogWarning("Main camera is not assigned.");
				return;
			}

			// Perform a raycast from the camera to the mouse position
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// Check if the ray hits something in the scene
			if (Physics.Raycast(ray, out hit))
			{
				// Calculate the direction vector from the shootPosition to the hit point
				Vector3 direction = hit.point - mainCharacter.position;
				direction.y = 0f; // Ensure the slash stays parallel to the ground

				// Calculate the rotation based on the direction vector
				Quaternion slashRotation = Quaternion.LookRotation(direction);

				// Instantiate the attack4Slash GameObject with the calculated rotation
				Instantiate(s_Skill2, mainCharacter.position, slashRotation);
			}
		}
        else
			return;
    }
	
	private void Skill2BowStart(AnimationEvent animationEvent)
    {
        if (thirdPersonShooterController.CurrentWeaponIndex == 2)
        {
            // Instantiate b_skill2_cast and store the instance
            skill2CastInstance = Instantiate(b_skill2_cast, mainCharacter.position, Quaternion.identity);
            
            // Invoke Skill2RainStart after 1 second
            Invoke("Skill2RainStart", 1f);
        }
    }

    private void Skill2RainStart()
    {
        // Check if skill2CastInstance is not null
        if (skill2CastInstance != null)
        {
            // Instantiate b_skill2_rain at the position of skill2CastInstance
            Instantiate(b_skill2_rain, skill2CastInstance.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("skill2CastInstance is null. Ensure it is instantiated correctly.");
        }
    }
	
	private void Skill2AxeStart(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 3)
		{
			a_skill2Object.SetActive(true);
			CancelInvoke("Skill2AxeEnd");
			Invoke("Skill2AxeEnd", 20f);
		}
	}

	private void Skill2AxeEnd()
	{    
		a_skill2Object.SetActive(false);
	}
	
	private void Skill3BowStart(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			b_skill3Object.SetActive(true);
			CancelInvoke("Skill3BowEnd");
			Invoke("Skill3BowEnd", 15f);
		}
	}

	private void Skill3BowEnd()
	{    
		b_skill3Object.SetActive(false);
	}
	
	private void BowThunderCastStart(AnimationEvent animationEvent)
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 2)
		{
			GameObject thunderCast = Instantiate(b_skill3Cast);
			thunderCast.transform.SetParent(mainCharacterBow);
			thunderCast.transform.localPosition = Vector3.zero;
			thunderCast.transform.rotation = Quaternion.identity;
		}
	}

	private void Skill3AxeStart()
    {
        // Check if skill2CastInstance is not null
        if (thirdPersonShooterController.CurrentWeaponIndex == 3)
        {
            // Instantiate b_skill2_rain at the position of skill2CastInstance
            Instantiate(a_skill3Shockwave, mainCharacter.position, Quaternion.identity);
        }
    }
	
	private void Skill3SwordStart()
	{
		if (thirdPersonShooterController.CurrentWeaponIndex == 1)
        {
			s_Skill3Shield.SetActive(false);
			s_Skill3Shield.SetActive(true);
			CanDamageFalse();
			CancelInvoke("Skill3SwordEnd");
			Invoke("Skill3SwordEnd", 5.5f);
		}
	}

	private void Skill3SwordEnd()
	{    
		s_Skill3Shield.SetActive(false);
		CanDamageTrue();
	}
	
}
