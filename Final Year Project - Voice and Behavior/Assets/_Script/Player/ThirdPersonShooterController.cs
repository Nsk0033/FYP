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
	[SerializeField] private GameObject axeGameObject;
	
	[Header("Character Weapon Index")]
	public int CurrentWeaponIndex = 1;
	
	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
    private float rangeLastUsedTime;
	private float meleeLastUsedTime;
	private float chargedLastUsedTime;
	private bool isUltiPlaying;
	
	
	
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
		if(animator.GetCurrentAnimatorStateInfo(1).IsName("Ultimate") || animator.GetCurrentAnimatorStateInfo(2).IsName("Ultimate") || animator.GetCurrentAnimatorStateInfo(3).IsName("Ultimate"))
		{
			isUltiPlaying = true;
		}
		else
			isUltiPlaying = false;
		
		
		if (Input.GetAxis("Mouse ScrollWheel") < 0f && !isUltiPlaying)
		{
			CurrentWeaponIndex--;
			if(CurrentWeaponIndex == 0)
			{
				CurrentWeaponIndex = 3;
			}
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0f && !isUltiPlaying)
		{
			CurrentWeaponIndex++;
			if(CurrentWeaponIndex == 4)
			{
				CurrentWeaponIndex = 1;
			}
		}
		
		SwitchWeapon();
		
		if (starterAssetsInputs.interact)
		{
			GameEventsManager.instance.inputEvents.SubmitPressed();
		}
    

    
        if (starterAssetsInputs.questLogToggle)
        {
            GameEventsManager.instance.inputEvents.QuestLogTogglePressed();
			starterAssetsInputs.questLogToggle = false;
			starterAssetsInputs.ToggleMenu();
        }
	}
	
	private void SwitchWeapon()
	{
		switch (CurrentWeaponIndex)
		{
			case 1:
				if(thirdPersonController.isDodgePlaying) 
					animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
				else
					animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));
				
				swordGameObject.SetActive(true);
				bowGameObject.SetActive(false);
				axeGameObject.SetActive(false);
				animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
				
				animator.SetLayerWeight(3, Mathf.Lerp(animator.GetLayerWeight(3), 0f, Time.deltaTime * 10f));
				animator.SetInteger("LayerSelection", 2); 
				
				break;
			case 2:
				if(thirdPersonController.isDodgePlaying) 
					animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
				else
					animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
				
				swordGameObject.SetActive(false);
				axeGameObject.SetActive(false);
				bowGameObject.SetActive(true);
				
				animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
				animator.SetLayerWeight(3, Mathf.Lerp(animator.GetLayerWeight(3), 0f, Time.deltaTime * 10f));
				animator.SetInteger("LayerSelection", 1);
				break;
			case 3:
				if(thirdPersonController.isDodgePlaying)
					animator.SetLayerWeight(3, Mathf.Lerp(animator.GetLayerWeight(3), 0f, Time.deltaTime * 10f));
				else
					animator.SetLayerWeight(3, Mathf.Lerp(animator.GetLayerWeight(3), 1f, Time.deltaTime * 10f));
				
				swordGameObject.SetActive(false);
				bowGameObject.SetActive(false);
				axeGameObject.SetActive(true);
				animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
				animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
				
				animator.SetInteger("LayerSelection", 3);
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
