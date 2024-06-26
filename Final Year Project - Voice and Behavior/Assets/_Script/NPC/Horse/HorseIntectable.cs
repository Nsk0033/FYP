using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseIntectable : MonoBehaviour, IInteractable
{
	[SerializeField] private string interactText;
    
	private Animator animator;
	
	
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(Transform interactorTransform)
    {
		bool isTalking = animator.GetCurrentAnimatorStateInfo(0).IsName("Rig_Rear_Up_VeryLight_Right");
		if(!isTalking) animator.SetTrigger("Talking");
    }
	
	public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
