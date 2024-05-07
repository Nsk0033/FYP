using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class PlayerAnimationEventTrigger : MonoBehaviour
{
	[Header("Character Components")]
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Transform mainCharacter;
	
	[Header("Character Sword Projectile")]
	[SerializeField] private GameObject s_attack4Slash;
	[SerializeField] private GameObject s_ChargedSlash1;
	[SerializeField] private GameObject s_ChargedSlash2;
	[SerializeField] private GameObject s_RangedSlash1;
	
	[Header("Character Bow Projectile")]
	[SerializeField] private float arrowSpread = 3f;
	[SerializeField] private int chargedAttackArrowCount = 3;
	[SerializeField] private GameObject b_attackProjectile;
	[SerializeField] private GameObject b_ChargedProjectile;
	[SerializeField] private GameObject b_ImpactParticle;
	
	[Header("Character Position")]
	[SerializeField] private Transform shootPosition;
	[SerializeField] private Transform shootPosition1;
	[SerializeField] private Transform shootPosition_upAir;
	[SerializeField] private Transform shootPosition_bowImpact;
	
	private ThirdPersonShooterController thirdPersonShooterController;
	private Animator animator;
	private StarterAssetsInputs starterAssetsInputs;
	//private ThirdPersonController thirdPersonController;
	
	private void Start()
	{
		thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
		animator = mainCharacter.GetComponent<Animator>();
		starterAssetsInputs = mainCharacter.GetComponent<StarterAssetsInputs>();
		//thirdPersonController = GetComponent<ThirdPersonController>();
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
				Vector3 direction = hit.point - shootPosition.position;
				direction.y = 0f; // Ensure the slash stays parallel to the ground

				// Calculate the rotation based on the direction vector
				Quaternion slashRotation = Quaternion.LookRotation(direction);

				// Instantiate the attack4Slash GameObject with the calculated rotation
				Instantiate(s_attack4Slash, shootPosition.position, slashRotation);
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
			// Get the y-axis rotation of the main character
			float characterRotationY = mainCharacter.rotation.eulerAngles.y;

			// Create a new rotation based on the main character's y-axis rotation
			Quaternion slashRotation = Quaternion.Euler(0f, characterRotationY, 0f);

			// Instantiate the ChargedSlash GameObject with the calculated rotation
			Instantiate(s_RangedSlash1, shootPosition1.position, slashRotation);
		}
		else
			return;
	}
	
	private void MeleeAttack1BowStart(AnimationEvent animationEvent)
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
			Quaternion rotationPositive3 = Quaternion.Euler(0f, 0f, arrowSpread);
			Quaternion rotationNegative3 = Quaternion.Euler(0f, 0f, -1f * arrowSpread);

			Vector3 aimDirPositive3 = rotationPositive3 * aimDir;
			Vector3 aimDirNegative3 = rotationNegative3 * aimDir;

			// Instantiate projectiles at shootPosition with the calculated aim directions
			Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
			Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDirPositive3, Vector3.up));
			Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDirNegative3, Vector3.up));
		}
		else
			return;
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
			Quaternion rotationNegative3 = Quaternion.Euler(0f, -1f * arrowSpread, 0f);

			Vector3 aimDirPositive3 = rotationPositive3 * aimDir;
			Vector3 aimDirNegative3 = rotationNegative3 * aimDir;

			// Instantiate projectiles at shootPosition with the calculated aim directions
			Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
			Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDirPositive3, Vector3.up));
			Instantiate(b_attackProjectile, shootPosition.position, Quaternion.LookRotation(aimDirNegative3, Vector3.up));
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
				Instantiate(b_ChargedProjectile, shootPosition_upAir.position, finalRotation);
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
}
