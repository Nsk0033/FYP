using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
	[Header("Character Weapon")]
	[SerializeField] private GameObject bowGameObject;
	[SerializeField] private GameObject swordGameObject;
	
	[Header("Character Weapon Index")]
	public int CurrentWeaponIndex = 1;
	
	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
    private float rangeLastUsedTime;
	private float meleeLastUsedTime;
	private float chargedLastUsedTime;
	
	
	private void Awake()
	{
		thirdPersonController = GetComponent<ThirdPersonController>();
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		animator = GetComponent<Animator>();
		
	}
	
	private void Start()
	{
		swordGameObject.SetActive(true);
		bowGameObject.SetActive(false);
	}
	
	private void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			CurrentWeaponIndex--;
			if(CurrentWeaponIndex == 0)
			{
				CurrentWeaponIndex = 2;
			}
		}
		
		SwitchWeapon();
	}
	
	private void SwitchWeapon()
	{
		switch (CurrentWeaponIndex)
		{
			case 1:
				swordGameObject.SetActive(true);
				bowGameObject.SetActive(false);
				animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
				animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));
				break;
			case 2:
				swordGameObject.SetActive(false);
				bowGameObject.SetActive(true);
				animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
				animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
				break;
			
		}
	}
	

	private void OnAttackStart(AnimationEvent animationEvent)
	{	
		swordGameObject.SetActive(true);
		Debug.Log("Show Sword");
		
	}
	
	private void OnAttackEnd(AnimationEvent animationEvent)
	{
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			swordGameObject.SetActive(false);
			Debug.Log("Hide Sword");
		}
	}
	
	/*private void OnChargedAttackStart(AnimationEvent animationEvent)   can use for general skill like henshin or ulti
	{
		meleeLayer = false;
		animator.SetLayerWeight(2, 0f);
	}
	
	private void OnChargedAttackEnd(AnimationEvent animationEvent)
	{
		meleeLayer = true;
		animator.SetLayerWeight(2, 0.8f);	
	}*/
	
	private void OnRangedAttackEnd(AnimationEvent animationEvent)
	{

		animator.ResetTrigger("Recoil");
		Debug.Log("Stop Furthure Shooting");
	}
	
}
