using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventTrigger : MonoBehaviour
{
	[Header("Character Components")]
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Transform mainCharacter;
	
	[Header("Character Projectile")]
	[SerializeField] private GameObject attack4Slash;
	[SerializeField] private GameObject ChargedSlash;
	
	[Header("Character Position")]
	[SerializeField] private Transform shootPosition;
	
	private ThirdPersonShooterController thirdPersonShooterController;
	
	private void Start()
	{
		thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
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
				Instantiate(attack4Slash, shootPosition.position, slashRotation);
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
			Instantiate(ChargedSlash, shootPosition.position, slashRotation);
		}
		else
			return;
	}

}
