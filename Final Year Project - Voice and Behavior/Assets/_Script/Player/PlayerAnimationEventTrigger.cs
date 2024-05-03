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
	
	[Header("Character Projectile")]
	[SerializeField] private GameObject m_attack4Slash;
	[SerializeField] private GameObject m_ChargedSlash1;
	[SerializeField] private GameObject m_ChargedSlash2;
	[SerializeField] private GameObject m_RangedSlash1;
	
	[Header("Character Position")]
	[SerializeField] private Transform shootPosition;
	[SerializeField] private Transform shootPosition1;
	
	private ThirdPersonShooterController thirdPersonShooterController;
	//private ThirdPersonController thirdPersonController;
	
	private void Start()
	{
		thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
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
				Instantiate(m_attack4Slash, shootPosition.position, slashRotation);
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
			Instantiate(m_ChargedSlash1, shootPosition.position, slashRotation);
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
			Instantiate(m_ChargedSlash2, shootPosition.position, slashRotation);
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
			Instantiate(m_RangedSlash1, shootPosition1.position, slashRotation);
		}
		else
			return;
	}
}
